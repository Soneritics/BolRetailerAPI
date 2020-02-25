using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.Exceptions;
using BolRetailerAPI.Models;

namespace BolRetailerAPI.Client
{
    /// <summary>
    /// Base client. Uses no authentication, only calls Http endpoints and processes the results.
    /// </summary>
    public abstract class ClientBase
    {
        public Error LastError { get; protected set; }
        public HttpStatusCode LastRequestStatus { get; protected set; }
        protected readonly HttpClient HttpClient;
        protected readonly IEndPoints EndPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        protected ClientBase(HttpClient httpClient, IEndPoints endPoints)
        {
            HttpClient = httpClient;
            EndPoints = endPoints;
        }

        /// <summary>
        /// Gets the API result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        public async Task<TResult> GetApiResult<TResult>(HttpMethod httpMethod, string endPoint, object post = null)
        {
            var request = await GetHttpRequestMessage(httpMethod, endPoint, post);
            var apiResult = await HttpClient.SendAsync(request);
            ProcessHttpHeaders(apiResult);
            return await GetProcessedResult<TResult>(apiResult);
        }

        /// <summary>
        /// Gets the HTTP request message.
        /// </summary>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        protected virtual async Task<HttpRequestMessage> GetHttpRequestMessage(HttpMethod httpMethod, string endPoint, object post = null)
        {
            return await Task.Run(() =>
            {
                var result = new HttpRequestMessage(httpMethod, endPoint);
                result.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (post != null)
                    result.Content = new StringContent(JsonConvert.SerializeObject(post));

                return result;
            });
        }

        /// <summary>
        /// Processes the HTTP headers.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        protected virtual void ProcessHttpHeaders(HttpResponseMessage httpResponseMessage)
        {
            LastRequestStatus = httpResponseMessage.StatusCode;
        }

        /// <summary>
        /// Gets the processed (deserialized) result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
        protected async Task<TResult> GetProcessedResult<TResult>(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
                throw await GetHttpException(httpResponseMessage);

            return await GetDeserializedResponse<TResult>(httpResponseMessage);
        }

        /// <summary>
        /// Gets the HTTP exception.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
        protected async Task<Exception> GetHttpException(HttpResponseMessage httpResponseMessage)
        {
            // First set the error message so it can be retrieved
            LastError = await GetDeserializedResponse<Error>(httpResponseMessage);

            // Throw specific exceptions for some headers.
            // https://api.bol.com/retailer/public/conventions/index.html
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedException();

                case HttpStatusCode.TooManyRequests:
                    return new TooManyRequestsException();

                // For every other unsuccessful HTTP code, throw an HttpRequestException
                default:
                    try
                    {
                        httpResponseMessage.EnsureSuccessStatusCode();
                    }
                    catch (Exception e)
                    {
                        return e;
                    }
                    break;
            }

            // This should not happen
            return new Exception($"HttpRequestException occured with message '{LastError.error_description}'");
        }

        /// <summary>
        /// Gets the deserialized response from the API.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
        protected async Task<TResult> GetDeserializedResponse<TResult>(HttpResponseMessage httpResponseMessage)
        {
            var msgString = await httpResponseMessage.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<TResult>(msgString);
            }
            catch
            {
                return default;
            }
        }
    }
}
