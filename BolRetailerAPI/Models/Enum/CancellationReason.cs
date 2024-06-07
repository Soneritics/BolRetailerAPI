namespace BolRetailerApi.Models.Enum;

public class CancellationReason
{
    public static readonly CancellationReason OutOfStock = new("OUT_OF_STOCK");
    public static readonly CancellationReason RequestedByCustomer = new("REQUESTED_BY_CUSTOMER");
    public static readonly CancellationReason BadCondition = new("BAD_CONDITION");
    public static readonly CancellationReason HigherShipcost = new("HIGHER_SHIPCOST");
    public static readonly CancellationReason IncorrectPrice = new("INCORRECT_PRICE");
    public static readonly CancellationReason NotAvailInTime = new("NOT_AVAIL_IN_TIME");
    public static readonly CancellationReason NoBolGuarantee = new("NO_BOL_GUARANTEE");
    public static readonly CancellationReason OrderedTwice = new("ORDERED_TWICE");
    public static readonly CancellationReason RetainItem = new("RETAIN_ITEM");
    public static readonly CancellationReason TechIssue = new("TECH_ISSUE");
    public static readonly CancellationReason UnfindableItem = new("UNFINDABLE_ITEM");
    public static readonly CancellationReason Other = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="CancellationReason" /> class.
    /// </summary>
    /// <param name="reason">The reason.</param>
    public CancellationReason(string reason = "OTHER")
    {
        ReasonValue = reason;
    }

    /// <summary>
    ///     Gets or sets the reason value.
    /// </summary>
    /// <value>
    ///     The reason value.
    /// </value>
    public string ReasonValue { get; set; }
}