namespace BolRetailerAPI.Models.Response.Orders
{
    public class BillingDetails
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string HouseNumberExtension { get; set; }
        public string ExtraAddressInformation { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string VatNumber { get; set; }
        public string KvkNumber { get; set; }
        public string OrderReference { get; set; }
    }
}