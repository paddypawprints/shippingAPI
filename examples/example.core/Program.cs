/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System;
using System.Text;
using System.Linq;
using System.Security;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Method;
using PitneyBowes.Developer.ShippingApi.Rules;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace example
{
    // Example logger - use your own
    public static class ApplicationLogging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    }

    class Program
    {
        private static ILogger Logger { get; } = ApplicationLogging.CreateLogger<Program>();
        private static IConfiguration Configuration { get; set; }
        private static SecureString ApiKey;

        static void Main(string[] args)
        {
            SetupConfigProvider();
            SetupApiKey();

            var sandbox = new Session() { EndPoint = "https://api-sandbox.pitneybowes.com", Requester = new ShippingApiHttpRequest() };

            // Initialize framework

            // Hook in your concrete classes
            Model.RegisterSerializationTypes(sandbox.SerializationRegistry);

            // Hook in your config provider
            sandbox.GetConfigItem = (s) => Configuration[s];

            // Hook in your logger
            sandbox.LogWarning = (s) => Logger.LogWarning(s);
            sandbox.LogError = (s) => Logger.LogError(s);
            sandbox.LogConfigError = (s) => Logger.LogCritical(s);
            sandbox.LogDebug = (s) => Logger.LogInformation(s);

            // Hook in your secure API key decryption
            sandbox.GetAPISecret = () => GetApiKey();

            //sandbox.Requester = new ShippingAPIMock();
            //sandbox.Record = true;

            Globals.DefaultSession = sandbox;

            // Create shipment
            var shipment = ShipmentFluent<Shipment>.Create()
                .ToAddress((Address)AddressFluent<Address>.Create()
                    .Company("ABC Company")
                    .Person("Rufous Sirius Canid", "323 555-1212", "rs.canid@gmail.com")
                    .Residential(false)
                    .AddressLines("643 Greenway RD")
                    .CityTown("Boone")
                    .StateProvince("NC")
                    .PostalCode("28607")
                    .CountryCode("US")
                    .Verify() // calls the service for address validation
                    )
                .MinimalAddressValidation("true")
                .ShipperRatePlan(Globals.DefaultSession.GetConfigItem("RatePlan"))
                .FromAddress((Address)AddressFluent<Address>.Create()
                    .Company("Pitney Bowes Inc.")
                    .AddressLines("27 Waterview Drive")
                    .Residential(false)
                    .CityTown("Shelton")
                    .StateProvince("CT")
                    .PostalCode("06484")
                    .CountryCode("US")
                    .Person("Paul Wright", "203-555-1213", "john.publica@pb.com")
                    )
                .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                    .Dimension(12, 12, 10)
                    .Weight(16m, UnitOfWeight.OZ)
                    )
                .Rates(RatesArrayFluent<Rates>.Create()
                    .USPSPriority<Rates, Parameter>()
                    .InductionPostalCode("06484")
                    )
                .Documents(DocumentsArrayFluent<Document>.Create()
                    .ShippingLabel()
                    )
                .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                    .ShipperId(sandbox.GetConfigItem("ShipperID"))
                    .AddToManifest()
                    )
                .TransactionId(Guid.NewGuid().ToString().Substring(15));

            var label = ShipmentsMethods.CreateShipment((Shipment)shipment).GetAwaiter().GetResult();

            if (label.Success)
            {
                Console.WriteLine(label.APIResponse.ParcelTrackingNumber);

                // Tracking

                var trackingRequest = new TrackingRequest
                {
                    Carrier = Carrier.USPS,
                    TrackingNumber = label.APIResponse.ParcelTrackingNumber
                };
                var trackingResponse = TrackingMethods.Tracking<TrackingStatus>(trackingRequest).GetAwaiter().GetResult();

                // Parcel Reprint
                var reprintRequest = new ReprintShipmentRequest() { Shipment = label.APIResponse.ShipmentId };

                var reprintResponse = ShipmentsMethods.ReprintShipment<Shipment>(reprintRequest).GetAwaiter().GetResult();

                // Write the label to disk
                foreach (var d in reprintResponse.APIResponse.Documents)
                {
                    if (d.ContentType == ContentType.BASE64 && d.FileFormat == FileFormat.PNG)
                    {
                        // Multiple page png document
                        DocumentsMethods.WriteToStream(d, null,

                            (stream, page) => // callback for each page
                            {
                                // create a new file

                                if (stream != null) stream.Dispose();
                                string fileName = string.Format("{0}{1}p{2}.{3}", Path.GetTempPath(), reprintResponse.APIResponse.ShipmentId, page.ToString(), d.FileFormat.ToString());
                                Console.WriteLine("Document written to " + fileName);
                                return new FileStream(fileName, FileMode.OpenOrCreate);

                            },
                            disposeStream: true
                            ).GetAwaiter().GetResult();
                    }
                    else
                    {
                        string fileName = string.Format("{0}{1}.{2}", Path.GetTempPath(), reprintResponse.APIResponse.ShipmentId, d.FileFormat.ToString());
                        using (StreamWriter sw = new StreamWriter(fileName))
                        {
                            DocumentsMethods.WriteToStream(d, sw.BaseStream).GetAwaiter().GetResult();
                            Console.WriteLine("Document written to " + fileName);
                        }
                    }
                }
            }

            // Create the manifest

            var manifest = ManifestFluent<Manifest>.Create()
                .Carrier(Carrier.USPS)
                .FromAddress(((Shipment)shipment).FromAddress)
                .InductionPostalCode("06484")
                .SubmissionDate(DateTime.Now)
                .AddParameter<Parameter>(ManifestParameter.SHIPPER_ID, sandbox.GetConfigItem("ShipperID"))
                .TransactionId(Guid.NewGuid().ToString().Substring(15));
            var manifestResponse = ManifestMethods.Create<Manifest>(manifest).GetAwaiter().GetResult();

            foreach (var d in manifestResponse.APIResponse.Documents)
            {
                string fileName = string.Format("{0}{1}.{2}", Path.GetTempPath(), manifestResponse.APIResponse.ManifestId, d.FileFormat.ToString());
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    Task.Run(() => DocumentsMethods.WriteToStream(d, sw.BaseStream));
                    Console.WriteLine("Document written to " + fileName);
                }
            }

            // Schedule a pickup

            var pickup = PickupFluent<Pickup>.Create()
                .Carrier(Carrier.USPS)
                .PackageLocation(PackageLocation.MailRoom)
                .PickupAddress(((Shipment)shipment).FromAddress)
                .PickupDate(DateTime.Now.AddDays(1))
                .AddPickupSummary<PickupCount, ParcelWeight>(Services.PM, 1, 16M, UnitOfWeight.OZ)
                .TransactionId(Guid.NewGuid().ToString().Substring(15));
            var pickupResponse = PickupMethods.Schedule<Pickup>(pickup).GetAwaiter().GetResult();

            // Cancel pickup

            if (pickupResponse.Success)
            {
                pickup.Cancel();
            }

            // Cancel the label 

            if (label.Success)
            {
                var cancelRequest = new CancelShipmentRequest
                {
                    Carrier = Carrier.USPS,
                    CancelInitiator = CancelInitiator.SHIPPER,
                    TransactionId = Guid.NewGuid().ToString().Substring(15),
                    ShipmentToCancel = label.APIResponse.ShipmentId
                };
                var cancelResponse = ShipmentsMethods.CancelShipment(cancelRequest).GetAwaiter().GetResult();
            }

            // Transaction report with IEnumerable

            var transactionsReportRequest = new ReportRequest()
            {
                FromDate = DateTimeOffset.Parse("6/30/2017"),
                ToDate = DateTimeOffset.Now,
                DeveloperId = sandbox.GetConfigItem("DeveloperID")
            };
            foreach (var t in TransactionsReport<Transaction>.Report(transactionsReportRequest, x => x.CreditCardFee == null || x.CreditCardFee > 10.0M))
            {
                Console.WriteLine(t.DestinationAddress);
            }

            // Transaction report with LINQ
            TransactionsReport<Transaction> report = new TransactionsReport<Transaction>(sandbox.GetConfigItem("DeveloperID"));
            var query = from transaction in report
                        where transaction.TransactionDateTime >= DateTimeOffset.Parse("6/30/2017") && transaction.TransactionDateTime <= DateTimeOffset.Now && transaction.TransactionType == TransactionType.POSTAGE_PRINT
                        select new { transaction.TransactionId };
            foreach (var obj in query)
                Console.WriteLine(obj);

            // Download the carrier rules
            var req = new RatingServicesRequest()
            {
                Carrier = Carrier.USPS,
                OriginCountryCode = "US",
                DestinationCountryCode = "US"
            };
            var res = CarrierRulesMethods.RatingServices(req).GetAwaiter().GetResult();
            if (res.Success)
            {
                // validate a shipment
                var v = new ShipmentValidator();
                if (!v.Validate((Shipment)shipment, res.APIResponse))
                {
                    Console.WriteLine("Shipment is not valid: {0}", v.Reason);
                }

                // do some interesting things with the rules
                var ruleRep = new RuleReport
                {
                    CarrierRules = new CarrierRule[1] { res.APIResponse }
                };

                // find possible services to fit a parcel
                var to = ((Shipment)shipment).ToAddress;
                var from = ((Shipment)shipment).FromAddress;
                var parcel = ((Shipment)shipment).Parcel;

                // match countries
                ruleRep.CarrierRuleFilter = (rule) => rule.OriginCountry == from.CountryCode && rule.DestinationCountry == to.CountryCode;
                // make sure parcel fits, is trackable and supports insurance, commercial base pricing
                ruleRep.ParcelTypeRuleFilter = (rule) =>
                    rule.FitsDimensions(parcel.Dimension) &&
                    rule.HoldsWeight(parcel.Weight) &&
                    !(rule.Trackable == Trackable.NON_TRACKABLE) &&
                    rule.SpecialServiceRules.ContainsKey(SpecialServiceCodes.Ins) &&
                    rule.RateTypeId == "CONTRACT_RATES";

                Shipment cheapestOption = null;
                foreach (var ruleItem in ruleRep)
                {

                    // create the shipment from looked up values
                    var newShipment = ShipmentFluent<Shipment>.Create()
                        .ToAddress(to)
                        .FromAddress(from)
                        .MinimalAddressValidation("true")
                        .ShipperRatePlan(Globals.DefaultSession.GetConfigItem("RatePlan"))
                        .Parcel(parcel)
                        .Rates(RatesArrayFluent<Rates>.Create()
                            .Add()
                            .RateFromRule(ruleItem.Item1, ruleItem.Item2, ruleItem.Item3)
                            .SpecialServiceFromRule<Rates, SpecialServices>(ruleItem.Item4)
                            .SuggestedTrackingServiceFromRule<Rates, SpecialServices>(ruleItem.Item3)
                            .InductionPostalCode(from.PostalCode)
                            )
                        .Documents(DocumentsArrayFluent<Document>.Create()
                            .ShippingLabel()
                            )
                        .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                            .ShipperId(sandbox.GetConfigItem("ShipperID"))
                            .AddToManifest()
                            )
                        .TransactionId(Guid.NewGuid().ToString().Substring(15));

                    // rate the parcel
                    var ratesResponse = RatesMethods.Rates<Shipment>((Shipment)newShipment).GetAwaiter().GetResult();
                    if (ratesResponse.Success)
                    {
                        if (cheapestOption == null || ratesResponse.APIResponse.Rates.First().TotalCarrierCharge < cheapestOption.Rates.First().TotalCarrierCharge)
                        {
                            cheapestOption = ratesResponse.APIResponse;
                        }
                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6} --- ${7}",
                            ruleItem.Item1.Carrier,
                            ruleItem.Item1.OriginCountry,
                            ruleItem.Item1.DestinationCountry,
                            ruleItem.Item2.ServiceId,
                            ruleItem.Item3.ParcelType,
                            ruleItem.Item3.RateTypeId,
                            ruleItem.Item4 == null ? String.Empty : ruleItem.Item4.SpecialServiceId.ToString(),
                            ratesResponse.APIResponse.Rates.First().TotalCarrierCharge);
                    }
                }
                //get the label
                cheapestOption.TransactionId = Guid.NewGuid().ToString().Substring(15);
                var newLabel = ShipmentsMethods.CreateShipment(cheapestOption).GetAwaiter().GetResult();
                if (newLabel.Success)
                {
                    string fileName = string.Format("{0}{1}.{2}", Path.GetTempPath(), newLabel.APIResponse.ShipmentId, "PDF");
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        DocumentsMethods.WriteToStream(newLabel.APIResponse.Documents.First(), sw.BaseStream).GetAwaiter().GetResult();
                        Console.WriteLine("Document written to " + fileName);
                    }
                }

            }
        }

        private static void SetupApiKey()
        {
            ApiKey = new SecureString();
            // This is not secure (obviously) - store your key encrypted. This is just to demonstrate the use of the session.
            foreach (var c in "wgNEtZkNbP0iV8h0".ToCharArray()) ApiKey.AppendChar(c);
        }

        private static StringBuilder GetApiKey()
        {
            var valuePtr = IntPtr.Zero;
            var s = new StringBuilder();
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(ApiKey);
                for (int i = 0; i < ApiKey.Length; i++)
                {
                    short unicodeChar = Marshal.ReadInt16(valuePtr, i * 2);
                    s.Append(Convert.ToChar(unicodeChar));
                }
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
            return s;
        }

        private static void SetupConfigProvider()
        {
            var configs = new Dictionary<string, string>
            {
                { "ApiKey", "Ci4vEAgBP8Aww7TBwGOKhr43uKTPNyfO" },
                { "RatePlan", "PP_SRP_NEWBLUE" },
                { "ShipperID", "9014888410" },
                { "DeveloperID", "46841939" }
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(configs);
            Configuration = configurationBuilder.Build();
        }
    }

    public static class ExtensionMethodExamples
    {
        public static RatesArrayFluent<T> SpecialServiceFromRule<T, S>(this RatesArrayFluent<T> f, SpecialServicesRule rule)
            where T : class, IRates, new()
            where S : class, ISpecialServices, new()
        {
            var ss = new S();
            ss.SpecialServiceId = rule.SpecialServiceId;
            ss.InputParameters = new List<Parameter>();

            if (rule.InputParameterRules == null)
            {
                ss.AddParameter(new Parameter("INPUT_VALUE", "0"));
            }
            else
            {
                foreach (var ip in rule.InputParameterRules.Values)
                {
                    ss.AddParameter(new Parameter(ip.Name, ip.MinValue.ToString()));
                }
            }
            f.SpecialService<S>(ss);
            return f;
        }
        public static RatesArrayFluent<T> RateFromRule<T>(this RatesArrayFluent<T> f, CarrierRule carrierRule, ServiceRule serviceRule, ParcelTypeRule parcelTypeRule)
            where T : class, IRates, new()
        {
            return f
                .Carrier(carrierRule.Carrier)
                .ParcelType(parcelTypeRule.ParcelType)
                .Service(serviceRule.ServiceId);
        }
        public static RatesArrayFluent<T> SuggestedTrackingServiceFromRule<T,S>(this RatesArrayFluent<T> f, ParcelTypeRule parcelTypeRule)
            where T : class, IRates, new()
            where S : class, ISpecialServices, new()
        {
            var r = f.Current();
            foreach (var s in r.SpecialServices)
            {
                if (s.SpecialServiceId == parcelTypeRule.SuggestedTrackableSpecialServiceId)
                    return f;
            }
            var trackingService = new S();
            trackingService.SpecialServiceId = parcelTypeRule.SuggestedTrackableSpecialServiceId;
            trackingService.InputParameters = new List<Parameter>();
            trackingService.AddParameter(new Parameter("INPUT_VALUE", "0"));
            r.AddSpecialservices(trackingService);
            return f;
        }
    }
}
