using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using System.IO;
using System.Text;



namespace tests
{
    class TestSession
    {
        static object lockObject = new object();

        static bool _initialized = false;
        static StringBuilder ApiKey = new StringBuilder("This is my secret");

        static public void Initialize(bool recordResults = false)
        {
            recordResults = false; //http methods hang. Unclear why.
            if (_initialized) return;
            lock (lockObject)
            {
                // Initialize framework
                if (_initialized) return;
                _initialized = true;
                var sandbox = new Session() { EndPoint = "https://api-sandbox.pitneybowes.com", Requester = new ShippingApiHttpRequest() };

                Model.RegisterSerializationTypes(sandbox.SerializationRegistry);

                // Configuration
                if (recordResults)
                {
                    sandbox.Record = true;
                }
                else
                {
                    sandbox.Requester = new ShippingAPIMock();
                }
                sandbox.RecordPath = string.Format("..{0}..{0}..{0}testData", Path.DirectorySeparatorChar);

                //*****************************************
                // Replace these with your own values
                //
                sandbox.AddConfigItem("ApiKey", "MyAPIKey");
                sandbox.GetAPISecret = () => ApiKey;
                sandbox.AddConfigItem("RatePlan", "PP_SRP_NEWBLUE");
                sandbox.AddConfigItem("ShipperID", "9014888410");
                sandbox.AddConfigItem("DeveloperID", "46841939");
                //******************************************

                Globals.DefaultSession = sandbox;

            }
        }
    }
}
