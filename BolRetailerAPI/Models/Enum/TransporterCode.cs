namespace BolRetailerApi.Models.Enum;

/// <summary>
///     List of all the transporter codes
/// </summary>
public class TransporterCode
{
    public static readonly TransporterCode Briefpost = new("BRIEFPOST");
    public static readonly TransporterCode Ups = new("UPS");
    public static readonly TransporterCode Tnt = new("TNT");
    public static readonly TransporterCode TntExtra = new("TNT-EXTRA");
    public static readonly TransporterCode TntBrief = new("TNT_BRIEF");
    public static readonly TransporterCode TntExpress = new("TNT-EXPRESS");
    public static readonly TransporterCode Dyl = new("DYL");
    public static readonly TransporterCode DpdNl = new("DPD-NL");
    public static readonly TransporterCode DpdBe = new("DPD-BE");
    public static readonly TransporterCode BpostBe = new("BPOST_BE");
    public static readonly TransporterCode BpostBrief = new("BPOST_BRIEF");
    public static readonly TransporterCode Dhlforyou = new("DHLFORYOU");
    public static readonly TransporterCode Gls = new("GLS");
    public static readonly TransporterCode FedexNl = new("FEDEX_NL");
    public static readonly TransporterCode FedexBe = new("FEDEX_BE");
    public static readonly TransporterCode Dhl = new("DHL");
    public static readonly TransporterCode DhlDe = new("DHL_DE");
    public static readonly TransporterCode DhlGlobalMail = new("DHL-GLOBAL-MAIL");
    public static readonly TransporterCode Tsn = new("TSN");
    public static readonly TransporterCode Fiege = new("FIEGE");
    public static readonly TransporterCode Transmission = new("TRANSMISSION");
    public static readonly TransporterCode ParcelNl = new("PARCEL-NL");
    public static readonly TransporterCode Logoix = new("LOGOIX");
    public static readonly TransporterCode Packs = new("PACKS");
    public static readonly TransporterCode Courier = new("COURIER");
    public static readonly TransporterCode Rjp = new("RJP");
    public static readonly TransporterCode Other = new("OTHER");

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransporterCode" /> class.
    /// </summary>
    /// <param name="transporterCodeValue">The transporter code value.</param>
    public TransporterCode(string transporterCodeValue)
    {
        TransporterCodeValue = transporterCodeValue;
    }

    /// <summary>
    ///     Gets or sets the transporter code value.
    /// </summary>
    /// <value>
    ///     The transporter code value.
    /// </value>
    public string TransporterCodeValue { get; set; }
}