namespace PitneyBowes.Developer.ShippingApi
{
    public interface ICustomsInfo
    {
        ReasonForExport ReasonForExport { get; set; }
        string reasonForExportExplanation { get; set; }
        string Comments { get; set; }
        string InvoiceNumber { get; set; }
        string ImporterCustomsReference { get; set; }
        string InsuredNumber { get; set; }
        decimal InsuredAmount { get; set; }
        decimal SdrValue { get; set; }
        string EELPFC { get; set; }
        string FromCustomsReference { get; set; }
        decimal CustomsDeclaredValue { get; set; }
        string CurrencyCode { get; set; }
        string LicenseNumber { get; set; }
        string CertificateNumber { get; set; }
    }
}