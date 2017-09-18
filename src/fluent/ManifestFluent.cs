using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    class ManifestFluent<T> where T : IManifest, new()
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

        ManifestFluent<T> Submit()
        {
            var response = ManifestMethods.Create(_manifest).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            return this;
        }

        ManifestFluent<T> Reprint(string manifestId)
        {
            var request = new ReprintManifestRequest() { ManifestId = manifestId };
            var response = ManifestMethods.Reprint<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            return this;
        }

        ManifestFluent<T> Retry(string originalId)
        {
            var request = new RetryManifestRequest() { OriginalTransactionId = originalId };
            var response = ManifestMethods.Retry<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            return this;
        }

        ManifestFluent<T> Carrier(Carrier c)
        {
            _manifest.Carrier = c;
            return this;
        }
        ManifestFluent<T> SubmissionDate(DateTimeOffset s)
        {
            _manifest.SubmissionDate = s;
            return this;
        }
        ManifestFluent<T> FromAddress(IAddress a)
        {
            _manifest.FromAddress = a;
            return this;
        }
        ManifestFluent<T> InductionPostalCode(string p)
        {
            _manifest.InductionPostalCode = p;
            return this;
        }
        ManifestFluent<T> ParcelTrackingNumbers(IEnumerable<string> tl)
        {
            foreach (var t in tl )
                _manifest.AddParcelTrackingNumber(t);
            return this;
        }
        ManifestFluent<T> AddParcelTrackingNumber(string t)
        {
            _manifest.AddParcelTrackingNumber(t);
            return this;
        }
        ManifestFluent<T> Parameters(IEnumerable<IParameter> pl)
        {
            foreach( var p in pl )
                _manifest.AddParameter(p);
            return this;
        }
        ManifestFluent<T> AddParameter(IParameter p)
        {
            _manifest.AddParameter(p);
            return this;
        }
    }

}

