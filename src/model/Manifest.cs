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
        virtual public Carrier Carrier { get;set;}
        virtual public DateTimeOffset SubmissionDate {get;set;}
        virtual public IAddress FromAddress {get;set;}
        virtual public string InductionPostalCode {get;set;}
        virtual public string ManifestId {get;set;}
        virtual public string ManifestTrackingNumber {get;set;}
        virtual public IEnumerable<string> ParcelTrackingNumbers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        virtual public IEnumerable<IParameter> Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        virtual public IEnumerable<IDocument> Documents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        virtual  public IDocument AddDocument(IDocument d)
        {
            throw new NotImplementedException();
        }

        public IParameter AddParameter(IParameter p)
        {
            throw new NotImplementedException();
        }

        public void AddParcelTrackingNumber(string t)
        {
            throw new NotImplementedException();
        }
    }
}