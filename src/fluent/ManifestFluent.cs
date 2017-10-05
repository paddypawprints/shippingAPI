using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class ManifestFluent<T> where T : IManifest, new()
    {
        private T _manifest;

        public static ManifestFluent<T> Create()
        {
            var p = new ManifestFluent<T>() { _manifest = new T() };
            return p;
        }

        private ManifestFluent()
        {

        }
        public static implicit operator T(ManifestFluent<T> m)
        {
            return (T)m._manifest;
        }

        public ManifestFluent<T> Submit()
        {
            var response = ManifestMethods.Create(_manifest).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            return this;
        }

        public ManifestFluent<T> Reprint(string manifestId)
        {
            var request = new ReprintManifestRequest() { ManifestId = manifestId };
            var response = ManifestMethods.Reprint<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            return this;
        }

        public ManifestFluent<T> Retry(string originalId)
        {
            var request = new RetryManifestRequest() { OriginalTransactionId = originalId };
            var response = ManifestMethods.Retry<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            return this;
        }

        public ManifestFluent<T> Carrier(Carrier c)
        {
            _manifest.Carrier = c;
            return this;
        }
        public ManifestFluent<T> SubmissionDate(DateTime s)
        {
            _manifest.SubmissionDate = s;
            return this;
        }
        public ManifestFluent<T> FromAddress(IAddress a)
        {
            _manifest.FromAddress = a;
            return this;
        }
        public ManifestFluent<T> InductionPostalCode(string p)
        {
            _manifest.InductionPostalCode = p;
            return this;
        }
        public ManifestFluent<T> ParcelTrackingNumbers(IEnumerable<string> tl)
        {
            foreach (var t in tl)
                _manifest.AddParcelTrackingNumber(t);
            return this;
        }
        public ManifestFluent<T> AddParcelTrackingNumber(string t)
        {
            _manifest.AddParcelTrackingNumber(t);
            return this;
        }
        public ManifestFluent<T> Parameters(IEnumerable<IParameter> pl)
        {
            foreach (var p in pl)
                _manifest.AddParameter(p);
            return this;
        }
        public ManifestFluent<T> AddParameter(IParameter p)
        {
            _manifest.AddParameter(p);
            return this;
        }
        public ManifestFluent<T> AddParameter<P>(string name, string value) where P : IParameter, new()
        {
            var p = new P
            {
                Name = name,
                Value = value
            };
            _manifest.AddParameter(p);
            return this;
        }
        public ManifestFluent<T> AddParameter<P>(ManifestParameter param, string value) where P : IParameter, new()
        {
            AddParameter<P>(param.ToString(), value);
            return this;
        }
        public ManifestFluent<T> TransactionId(string t)
        {
            _manifest.TransactionId = t;
            return this;
        }

    }
}

