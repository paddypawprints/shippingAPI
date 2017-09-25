using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Method;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

using static PitneyBowes.Developer.ShippingApi.SessionDefaults;

namespace tests
{
    class Session
    {
        static object lockObject = new object();

        static bool _initialized = false;

        static public void Initialize(bool recordResults = false)
        {
            recordResults = false; //http methods hang. Unclear why.
            if (_initialized) return;
            lock (lockObject)
            {
                // Initialize framework
                if (_initialized) return;
                _initialized = true;
                Init();
                Model.RegisterSerializationTypes(DefaultSession);

                // Configuration
                DefaultSession = "sandbox";
                if (recordResults)
                {
                    DefaultSession.Record = true;
                }
                else
                {
                    DefaultSession.Requestor = new ShippingAPIMock();
                }
                DefaultSession.RecordPath = string.Format("..{0}..{0}..{0}testData", Path.DirectorySeparatorChar);

                //*****************************************
                // Replace these with your own values
                //
                AddConfigItem("ApiKey", "MyAPIKey");
                DefaultSession.GetAPISecret = () => "This is my secret".ToCharArray();
                AddConfigItem("RatePlan", "PP_SRP_NEWBLUE");
                AddConfigItem("ShipperID", "9014888410");
                AddConfigItem("DeveloperID", "46841939");
                //******************************************

                // Authenticate
                var tokenResponse = TokenMethods.token<Token>().GetAwaiter().GetResult();

            }
        }
    }
}
