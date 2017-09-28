using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Method;
using System;
using System.Linq;

using static PitneyBowes.Developer.ShippingApi.SessionDefaults;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            // Authenticate
            //var tokenResponse = TokenMethods.token<Token>().GetAwaiter().GetResult();

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
            shipment.ShipperRatePlan = DefaultSession.GetConfigItem("RatePlan");

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
                .ShipperId(DefaultSession.GetConfigItem("ShipperID"))
                .AddToManifest();

            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);

            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            if (label.Success) Console.WriteLine(label.APIResponse.ParcelTrackingNumber);

            // Transaction report
            /*
            var transactionsReportRequest = new ReportRequest()
            {
                FromDate = DateTimeOffset.Parse("6/30/2017"),
                ToDate = DateTimeOffset.Now,
                DeveloperId = DefaultSession.GetConfigItem("DeveloperID")
            };
            foreach (var t in TransactionsReport<Transaction>.Report(transactionsReportRequest, x => x.CreditCardFee == null || x.CreditCardFee > 10.0M ))
            {
                Console.WriteLine(t.DestinationAddress);
            }

            TransactionsReport<Transaction> report = new TransactionsReport<Transaction>(DefaultSession.GetConfigItem("DeveloperID"));
            var query = from transaction in report
                        where transaction.TransactionDateTime >= DateTimeOffset.Parse("6/30/2017") && transaction.TransactionDateTime <= DateTimeOffset.Now
                        select new { transaction.DestinationCountry };

            foreach (var obj in query)
                Console.WriteLine(obj);
                */
            var req = new RatingServicesRequest()
            {

                Carrier = Carrier.USPS,
                OriginCountryCode = "US",
                DestinationCountryCode = "US"
            };
            var res = CarrierRulesMethods.RatingServices<CarrierRule[]>(req).GetAwaiter().GetResult();

        }

        static void Initialize()
        {
            // Initialize framework
            Init();
            Model.RegisterSerializationTypes(DefaultSession);

            // Configuration
            DefaultSession = "sandbox";
            DefaultSession.LogWarning = (s) => Console.WriteLine("Warning:" + s);
            DefaultSession.LogError = (s) => Console.WriteLine("Error:" + s);
            DefaultSession.LogConfigError = (s) => Console.WriteLine("Bad code:" + s);
            DefaultSession.LogDebug = (s) => Console.WriteLine(s);
            //DefaultSession.Requestor = new ShippingAPIMock();
            DefaultSession.Record = true;
#if NET_45
#else
            DefaultSession.TraceSerialization = true;
#endif
            //*****************************************
            // Replace these with your own values
            //
            AddConfigItem("ApiKey", "Ci4vEAgBP8Aww7TBwGOKhr43uKTPNyfO");
            DefaultSession.GetAPISecret = () => "wgNEtZkNbP0iV8h0".ToCharArray();
            AddConfigItem("RatePlan", "PP_SRP_NEWBLUE");
            AddConfigItem("ShipperID", "9014888410");
            AddConfigItem("DeveloperID", "46841939");
            //******************************************
        }
    }
}
