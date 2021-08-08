using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Status
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
        public List<Link> Links { get; set; }
    }
}
