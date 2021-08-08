using System.Collections.Generic;

namespace BolRetailerAPI.Models.Status
{
    public class Error
    {
        public string error { get; set; }
        public string error_description { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string detail { get; set; }
        public string host { get; set; }
        public string instance { get; set; }
        public List<Violation> violations { get; set; }
    }
}
