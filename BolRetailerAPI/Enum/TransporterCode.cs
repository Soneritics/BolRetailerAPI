namespace BolRetailerAPI.Enum
{
    /// <summary>
    /// List of all the transporter codes
    /// </summary>
    public class TransporterCode
    {
        public static readonly TransporterCode Briefpost = new TransporterCode("BRIEFPOST");
        public static readonly TransporterCode Ups = new TransporterCode("UPS");
        public static readonly TransporterCode Tnt = new TransporterCode("TNT");
        public static readonly TransporterCode TntExtra = new TransporterCode("TNT-EXTRA");
        public static readonly TransporterCode TntBrief = new TransporterCode("TNT_BRIEF");
        public static readonly TransporterCode TntExpress = new TransporterCode("TNT-EXPRESS");
        public static readonly TransporterCode Dyl = new TransporterCode("DYL");
        public static readonly TransporterCode DpdNl = new TransporterCode("DPD-NL");
        public static readonly TransporterCode DpdBe = new TransporterCode("DPD-BE");
        public static readonly TransporterCode BpostBe = new TransporterCode("BPOST_BE");
        public static readonly TransporterCode BpostBrief = new TransporterCode("BPOST_BRIEF");
        public static readonly TransporterCode Dhlforyou = new TransporterCode("DHLFORYOU");
        public static readonly TransporterCode Gls = new TransporterCode("GLS");
        public static readonly TransporterCode FedexNl = new TransporterCode("FEDEX_NL");
        public static readonly TransporterCode FedexBe = new TransporterCode("FEDEX_BE");
        public static readonly TransporterCode Dhl = new TransporterCode("DHL");
        public static readonly TransporterCode DhlDe = new TransporterCode("DHL_DE");
        public static readonly TransporterCode DhlGlobalMail = new TransporterCode("DHL-GLOBAL-MAIL");
        public static readonly TransporterCode Tsn = new TransporterCode("TSN");
        public static readonly TransporterCode Fiege = new TransporterCode("FIEGE");
        public static readonly TransporterCode Transmission = new TransporterCode("TRANSMISSION");
        public static readonly TransporterCode ParcelNl = new TransporterCode("PARCEL-NL");
        public static readonly TransporterCode Logoix = new TransporterCode("LOGOIX");
        public static readonly TransporterCode Packs = new TransporterCode("PACKS");
        public static readonly TransporterCode Courier = new TransporterCode("COURIER");
        public static readonly TransporterCode Rjp = new TransporterCode("RJP");
        public static readonly TransporterCode Other = new TransporterCode("OTHER");

        /// <summary>
        /// Gets or sets the transporter code value.
        /// </summary>
        /// <value>
        /// The transporter code value.
        /// </value>
        public string TransporterCodeValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransporterCode"/> class.
        /// </summary>
        /// <param name="transporterCodeValue">The transporter code value.</param>
        public TransporterCode(string transporterCodeValue)
        {
            TransporterCodeValue = transporterCodeValue;
        }
    }
}
