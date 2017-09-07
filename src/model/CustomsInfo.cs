

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CustomsInfo : ICustomsInfo
    {

        virtual public ReasonForExport ReasonForExport { get; set;}

        virtual public string reasonForExportExplanation { get; set;}

        virtual public string Comments { get;set;}

        virtual public string InvoiceNumber {get;set;}

        virtual public string ImporterCustomsReference {get;set;}

        virtual public string InsuredNumber {get;set;}

        virtual public decimal InsuredAmount {get;set;}

        virtual public decimal SdrValue { get;set;}

        virtual public string EELPFC {get;set;}

        virtual public string FromCustomsReference {get;set;}

        virtual public decimal CustomsDeclaredValue { get;set;}

        virtual public string CurrencyCode {get;set;}

        virtual public string LicenseNumber {get;set;}

        virtual public string CertificateNumber {get;set;}
    }
}