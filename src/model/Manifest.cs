using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Manifest : IManifest
    {
        public Manifest()
        {
            //ParcelTrackingNumbers = new List<string>();
            //Parameters = new List<Parameter>();
            //Documents = new List<Document>();
        }
        virtual public string TransactionId { get; set; }
        virtual public Carrier Carrier { get;set;}
        virtual public DateTimeOffset SubmissionDate {get;set;}
        virtual public IAddress FromAddress {get;set;}
        virtual public string InductionPostalCode {get;set;}
        virtual public string ManifestId {get;set;}
        virtual public string ManifestTrackingNumber {get;set;}
        virtual public IEnumerable<string> ParcelTrackingNumbers { get; set; }
        virtual public IEnumerable<IParameter> Parameters { get; set; }
        virtual public IEnumerable<IDocument> Documents { get; set; }

        virtual  public IDocument AddDocument(IDocument d)
        {
            return ModelHelper.AddToEnumerable<IDocument, Document>(d, () => Documents, (v) => Documents = v );
        }

        public IParameter AddParameter(IParameter p)
        {
            return ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => Parameters, (v) => Parameters = v);
        }

        public void AddParcelTrackingNumber(string t)
        {
            ModelHelper.AddToEnumerable<string, string>(t, () => ParcelTrackingNumbers, (v) => ParcelTrackingNumbers = v);
        }
    }
}