using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Manifest
    {
        public Manifest()
        {
            ParcelTrackingNumbers = new List<string>();
            Parameters = new List<Parameter>();
            Documents = new List<Document>();
        }
        virtual public Carrier Carrier { get;set;}
        virtual public DateTimeOffset SubmissionDate {get;set;}
        virtual public IAddress FromAddress {get;set;}
        virtual public string InductionPostalCode {get;set;}
        virtual public IEnumerable<string> ParcelTrackingNumbers {get;}
        virtual public IEnumerable<IParameter> Parameters { get; }
        virtual public string ManifestId {get;set;}
        virtual public string ManifestTrackingNumber {get;set;}
        virtual public IEnumerable<IDocument> Documents { get; }
    }
}