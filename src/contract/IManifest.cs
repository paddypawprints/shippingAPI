using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IManifest
    {
        string TransactionId { get; set; }
        Carrier Carrier { get; set; }
        DateTimeOffset SubmissionDate { get; set; }
        IAddress FromAddress { get; set; }
        string InductionPostalCode { get; set; }
        IEnumerable<string> ParcelTrackingNumbers { get; set; }
        void AddParcelTrackingNumber(string t);
        IEnumerable<IParameter> Parameters { get; set; }
        IParameter AddParameter(IParameter p);
        string ManifestId { get; set; }
        string ManifestTrackingNumber { get; set; }
        IEnumerable<IDocument> Documents { get; set; }
        IDocument AddDocument(IDocument d);
    }
}