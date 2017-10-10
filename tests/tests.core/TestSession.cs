/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

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
