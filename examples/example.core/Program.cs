using System;
using System.Text;
using System.Linq;
using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Method;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Security;
using System.Runtime.InteropServices;

namespace example
{
    public static class ApplicationLogging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>() =>
          LoggerFactory.CreateLogger<T>();
    }

    class Program
    {
        private static ILogger Logger { get; } =  ApplicationLogging.CreateLogger<Program>();
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
            var shipment = new CreateShipmentRequest<Shipment>()
            {
                ToAddress = (Address)AddressFluent<Address>.Create().Company("ABC Company")
                    .Person("Rufous Sirius Canid","323 555-1212","rs.canid@gmail.com")
                    .Residential(false)
                    .AddressLines("643 Greenway RD")
                    .CityTown("Boone")
                    .StateProvince("NC")
                    .PostalCode("28607")
                    .CountryCode("US")
                    .Verify() // calls the service for address validation
            };

            shipment.MinimalAddressValidation = "true";
            shipment.ShipperRatePlan = Globals.DefaultSession.GetConfigItem("RatePlan");

            shipment.FromAddress = (Address)AddressFluent<Address>.Create()
                .Company("Pitney Bowes Inc.")
                .AddressLines("27 Waterview Drive")
                .Residential(false)
                .CityTown("Shelton")
                .StateProvince("CT")
                .PostalCode("06484")
                .CountryCode("US")
                .Person("Paul Wright", "203-555-1213","john.publica@pb.com");

            shipment.Parcel = (Parcel)ParcelFluent<Parcel>.Create()
                .Dimension(12, 12, 10)
                .Weight(16m, UnitOfWeight.OZ);

            shipment.Rates = RatesArrayFluent<Rates>.Create()
                .USPSPriority()
                .InductionPostalCode("06484");

            shipment.Documents = DocumentsArrayFluent<Document>.Create()
                .ShippingLabel();

            shipment.ShipmentOptions = ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                .ShipperId(sandbox.GetConfigItem("ShipperID"))
                .AddToManifest();

            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);

            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            if (label.Success)
            {
                Console.WriteLine(label.APIResponse.ParcelTrackingNumber);

                var reprintRequest = new ReprintShipmentRequest
                {
                    Shipment = label.APIResponse.ShipmentId
                };

                var reprintResponse = ShipmentsMethods.ReprintShipment<Shipment>(reprintRequest).GetAwaiter().GetResult();

                /*var cancelRequest = new CancelShipmentRequest
                {
                    Carrier = Carrier.USPS,
                    CancelInitiator = CancelInitiator.SHIPPER,
                    TransactionId = Guid.NewGuid().ToString().Substring(15),
                    ShipmentToCancel = label.APIResponse.ShipmentId
                };

                var cancelResponse = ShipmentsMethods.CancelShipment(cancelRequest).GetAwaiter().GetResult();
                */


            }

            var manifest = ManifestFluent<Manifest>.Create()
                .Carrier(Carrier.USPS)
                .FromAddress(shipment.FromAddress)
                .InductionPostalCode("06484")
                .SubmissionDate(DateTime.Now)
                .AddParameter<Parameter>( ManifestParameter.SHIPPER_ID, sandbox.GetConfigItem("ShipperID"))
                .TransactionId(Guid.NewGuid().ToString().Substring(15));

            var manifestResponse = ManifestMethods.Create<Manifest>(manifest).GetAwaiter().GetResult();

            // Transaction report
 
            var transactionsReportRequest = new ReportRequest()
            {
                FromDate = DateTimeOffset.Parse("6/30/2017"),
                ToDate = DateTimeOffset.Now,
                DeveloperId = sandbox.GetConfigItem("DeveloperID")
            };
            foreach (var t in TransactionsReport<Transaction>.Report(transactionsReportRequest, x => x.CreditCardFee == null || x.CreditCardFee > 10.0M ))
            {
                Console.WriteLine(t.DestinationAddress);
            }

            TransactionsReport<Transaction> report = new TransactionsReport<Transaction>(sandbox.GetConfigItem("DeveloperID"));
            var query = from transaction in report
                        where transaction.TransactionDateTime >= DateTimeOffset.Parse("6/30/2017") && transaction.TransactionDateTime <= DateTimeOffset.Now && transaction.TransactionType == TransactionType.POSTAGE_PRINT
                        select new { transaction.TransactionId };

            foreach (var obj in query)
                Console.WriteLine(obj);
                
            var req = new RatingServicesRequest()
            {
                Carrier = Carrier.USPS,
                OriginCountryCode = "US",
                DestinationCountryCode = "US"
            };
            var res = CarrierRulesMethods.RatingServices<CarrierRule[]>(req).GetAwaiter().GetResult();

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
}
