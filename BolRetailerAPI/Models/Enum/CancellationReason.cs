namespace BolRetailerApi.Models.Enum
{
    public class CancellationReason
    {
        public static readonly CancellationReason OutOfStock = new CancellationReason("OUT_OF_STOCK");
        public static readonly CancellationReason RequestedByCustomer = new CancellationReason("REQUESTED_BY_CUSTOMER");
        public static readonly CancellationReason BadCondition = new CancellationReason("BAD_CONDITION");
        public static readonly CancellationReason HigherShipcost = new CancellationReason("HIGHER_SHIPCOST");
        public static readonly CancellationReason IncorrectPrice = new CancellationReason("INCORRECT_PRICE");
        public static readonly CancellationReason NotAvailInTime = new CancellationReason("NOT_AVAIL_IN_TIME");
        public static readonly CancellationReason NoBolGuarantee = new CancellationReason("NO_BOL_GUARANTEE");
        public static readonly CancellationReason OrderedTwice = new CancellationReason("ORDERED_TWICE");
        public static readonly CancellationReason RetainItem = new CancellationReason("RETAIN_ITEM");
        public static readonly CancellationReason TechIssue = new CancellationReason("TECH_ISSUE");
        public static readonly CancellationReason UnfindableItem = new CancellationReason("UNFINDABLE_ITEM");
        public static readonly CancellationReason Other = new CancellationReason("OTHER");

        /// <summary>
        /// Gets or sets the reason value.
        /// </summary>
        /// <value>
        /// The reason value.
        /// </value>
        public string ReasonValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationReason"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public CancellationReason(string reason = "OTHER")
        {
            ReasonValue = reason;
        }
    }
}
