using System;

namespace BolRetailerAPI.Models
{
    public class StatusResponse
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreateTimestamp { get; set; }
    }
}
