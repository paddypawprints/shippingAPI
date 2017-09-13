using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using System;
using System.Linq;
using static PitneyBowes.Developer.ShippingApi.ShippingApi;

namespace example
{


    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            // Authenticate
            var tokenResponse = TokenMethods.token<Token>().GetAwaiter().GetResult();

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
            shipment.ShipperRatePlan = "PP_SRP_NEWBLUE";

            shipment.FromAddress = (Address)AddressFluent<Address>.Create()
                .HeadOffice()
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
                .ShipperId("9014888410")
                .AddToManifest();

            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);

            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            if (label.Success) Console.WriteLine(label.APIResponse.ParcelTrackingNumber);

            // Transaction report

            var transactionsReportRequest = new ReportRequest()
            {
                FromDate = DateTimeOffset.Parse("6/30/2017"),
                ToDate = DateTimeOffset.Now,
                DeveloperId = "46841939"
            };
            foreach (var t in TransactionsReport<Transaction>.Report(transactionsReportRequest, x => x.CreditCardFee == null || x.CreditCardFee > 10.0M ))
            {
                Console.WriteLine(t);
            }

            TransactionsReport<Transaction> report = new TransactionsReport<Transaction>("46841939");
            var query = from transaction in report
                        where transaction.TransactionDateTime >= DateTimeOffset.Parse("6/30/2017") && transaction.TransactionDateTime <= DateTimeOffset.Now
                        select new { transaction.MerchantId };

            foreach (var obj in query)
                Console.WriteLine(obj);
        }

        static void ExtensionMethod()
        {
            Address address = AddressFluent<Address>.Create().USParse("101 Jenkins Pl, Santa Clara, CA 95051")
                .Status(AddressStatus.NOT_CHANGED);
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
            DefaultSession.GetAPISecret = () => "wgNEtZkNbP0iV8h0".ToCharArray();
            AddConfigItem("ApiKey", "Ci4vEAgBP8Aww7TBwGOKhr43uKTPNyfO");
            AddConfigItem("RatePlan", "PP_SRP_NEWBLUE");


            DefaultSession.TraceSerialization = true;
        }
    }

    static class Extensions
    {
        public static AddressFluent<Address> USParse(this AddressFluent<Address> f, string s)
        {
            var parser = new AddressParser.AddressParser();
            var address = parser.ParseAddress(s);
            f.CityTown(address.City);
            f.CountryCode("US");
            f.PostalCode(address.Zip);
            f.AddressLines(address.StreetLine);
            return f;

        }

        public static RatesArrayFluent<Rates> USPSPriority(this RatesArrayFluent<Rates> f)
        {
            return f.Add().Carrier(Carrier.USPS)
                .ParcelType(USPSParcelType.PKG)
                .Service(USPSServices.PM)
                .SpecialService<SpecialServices>(USPSSpecialServiceCodes.DelCon, 0M, new Parameter("INPUT_VALUE", "0"));
        }

        public static RatesArrayFluent<Rates> Insurance(this RatesArrayFluent<Rates> f, decimal amount)
        {
            return f.SpecialService<SpecialServices>(USPSSpecialServiceCodes.Ins, 0M, new Parameter("INPUT_VALUE", amount.ToString()));
        }

        public static AddressFluent<Address> HeadOffice( this AddressFluent<Address> f)
        {
            return f.Company("Pitney Bowes Inc.")
                .AddressLines("27 Waterview Drive")
                .Residential(false)
                .CityTown("Shelton")
                .StateProvince("CT")
                .PostalCode("06484")
                .CountryCode("US");
        }

        public static AddressFluent<Address> Person( this AddressFluent<Address> f, string name, string phone = null, string email = null)
        {
            return f.Name(name).Phone(phone).Email(email);
        }

        public static DocumentsArrayFluent<Document> ShippingLabel(this DocumentsArrayFluent<Document> f, ContentType contentType = ContentType.URL, Size size = Size.DOC_8X11, FileFormat fileFormat = FileFormat.PDF)
        { 
            return f.Add()
                .DocumentType(DocumentType.SHIPPING_LABEL)
                .ContentType(contentType)
                .Size(size)
                .FileFormat(fileFormat)
                .PrintDialogOption(PrintDialogOption.NO_PRINT_DIALOG);
        }
        public static ShipmentOptionsArrayFluent<ShipmentOptions> ShipperId(this ShipmentOptionsArrayFluent<ShipmentOptions> f, string shipperId)
        {
            return f.Add().Option(ShipmentOption.SHIPPER_ID, shipperId);
        }
        public static ShipmentOptionsArrayFluent<ShipmentOptions> AddToManifest(this ShipmentOptionsArrayFluent<ShipmentOptions> f)
        {
            return f.Add().Option(ShipmentOption.ADD_TO_MANIFEST, "true");
        }
        public static ShipmentOptionsArrayFluent<ShipmentOptions> MinimalAddressvalidation(this ShipmentOptionsArrayFluent<ShipmentOptions> f)
        {
            return f.Add().Option(ShipmentOption.MINIMAL_ADDRESS_VALIDATION, "true");
        }
        public static ShipmentOptionsArrayFluent<ShipmentOptions> AddOption(this ShipmentOptionsArrayFluent<ShipmentOptions> f, ShipmentOption option, string value)
        {
            return f.Add().Option(option, value);
        }
    }
}
