using System;

namespace BolRetailerAPI.Models
{
    /// <summary>
    /// Keeps track of the rate limits.
    /// </summary>
    public class RateLimits
    {
        public int Limit { get; set; }
        public int Remaining { get; set; }
        public DateTime ResetsAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Determines whether the current rate limit is (still) valid or that is already has been reset.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if still valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsStillValid()
        {
            return ResetsAt > DateTime.Now;
        }
    }
}
