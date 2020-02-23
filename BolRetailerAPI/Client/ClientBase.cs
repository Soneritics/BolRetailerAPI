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
    public class ClientBase
    {
        public Error LastError { get; protected set; }
        public HttpStatusCode LastRequestStatus { get; protected set; }
        protected readonly HttpClient HttpClient;
        protected readonly IEndPoints EndPoints;

        public ClientBase(HttpClient httpClient, IEndPoints endPoints)
        {
            HttpClient = httpClient;
            EndPoints = endPoints;

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual async Task<TResult> GetApiResultViaGet<TResult>(string endPoint)
        {
            var apiResult = await HttpClient.GetAsync(endPoint);
            return await GetProcessedResult<TResult>(apiResult);
        }

        public virtual async Task<TResult> GetApiResultViaPost<TResult>(string endPoint, object post)
        {
            var content = JsonConvert.SerializeObject(post);
            var apiResult = await HttpClient.PostAsync(endPoint, new StringContent(content));
            return await GetProcessedResult<TResult>(apiResult);
        }

        protected async Task<TResult> GetProcessedResult<TResult>(HttpResponseMessage httpResponseMessage)
        {
            LastRequestStatus = httpResponseMessage.StatusCode;

            if (!httpResponseMessage.IsSuccessStatusCode)
                ThrowHttpException(httpResponseMessage);

            return await GetDeserializedResponse<TResult>(httpResponseMessage);
        }

        protected async void ThrowHttpException(HttpResponseMessage httpResponseMessage)
        {
            // First set the error message so it can be retrieved
            LastError = await GetDeserializedResponse<Error>(httpResponseMessage);

            // Throw specific exceptions for some headers.
            // https://api.bol.com/retailer/public/conventions/index.html
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();

                case HttpStatusCode.TooManyRequests:
                    throw new TooManyRequestsException();

                // For every other unsuccessful HTTP code, throw an HttpRequestException
                default:
                    httpResponseMessage.EnsureSuccessStatusCode();
                    break;
            }

            // This should not happen
            throw new Exception($"HttpRequestException occured with message '{LastError.error_description}'");
        }

        protected async Task<TResult> GetDeserializedResponse<TResult>(HttpResponseMessage httpResponseMessage)
        {
            var msgString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(msgString);
        }
    }
}
