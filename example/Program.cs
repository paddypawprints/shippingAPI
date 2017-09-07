using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using System;
using System.Linq;
using static PitneyBowes.Developer.ShippingApi.ShippingApi;

namespace example
{
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
                .SpecialService<SpecialServices>(USPSSpecialServiceCodes.Ins, 0M, new Parameter("INPUT_VALUE", "500"))
                .SpecialService<SpecialServices>(USPSSpecialServiceCodes.DelCon, 0M, new Parameter("INPUT_VALUE", "0"));
        }

        public static AddressFluent<Address> HeadOffice( this AddressFluent<Address> f)
        {
            return f.AddressLines("12FL", "3000 Summer St.")
                .CityTown("Stamford")
                .StateProvince("CT")
                .PostalCode("05051")
                .CountryCode("US");
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            // Initialize framework
            Init();
            Model.RegisterSerializationTypes(DefaultSession);

            // Configuration
            DefaultSession = "sandbox";
            DefaultSession.LogWarning = (s)=> Console.WriteLine("Warning:" + s);
            DefaultSession.LogError = (s)=> Console.WriteLine("Error:" + s);
            DefaultSession.LogConfigError = (s) => Console.WriteLine("Bad code:" + s);
            DefaultSession.LogDebug = (s) => Console.WriteLine( s);
            DefaultSession.GetAPISecret = ()=> "wgNEtZkNbP0iV8h0".ToCharArray();
            AddConfigItem("ApiKey", "Ci4vEAgBP8Aww7TBwGOKhr43uKTPNyfO");
            AddConfigItem("RatePlan", /*"PP_SRP_CBP"*/"");


            DefaultSession.TraceSerialization = true;


            // Authenticate
            var tokenResponse = TokenMethods.token<Token>().GetAwaiter().GetResult();

            // Create shipment
            var shipment = new CreateShipmentRequest<Shipment>()
            {
                ToAddress = (Address)AddressFluent<Address>.Create().AddressLines("101 Jenkins Place")
                    .CityTown("Santa Clara")
                    .StateProvince("CA")
                    .PostalCode("95051")
                    .CountryCode("US")
            };
            shipment.FromAddress = (Address)AddressFluent<Address>.Create().HeadOffice();

            shipment.Parcel = (Parcel)ParcelFluent<Parcel>.Create().Dimension(12, 12, 10)
                .Weight(16m, UnitOfWeight.OZ);
            shipment.Rates = RatesArrayFluent<Rates>.Create().USPSPriority().InductionPostalCode("06484-4301");
            shipment.Documents = DocumentsArrayFluent<Document>.Create().Add()
                .DocumentType(DocumentType.SHIPPING_LABEL)
                .ContentType(ContentType.URL)
                .Size(Size.DOC_8X11)
                .FileFormat(FileFormat.PDF)
                .PrintDialogOption(PrintDialogOption.EMBED_PRINT_DIALOG);
            shipment.ShipmentOptions = ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                .Add().Option(ShipmentOption.SHIPPER_ID, "9014888410")
                .Add().Option(ShipmentOption.ADD_TO_MANIFEST, "true")
                .Add().Option(ShipmentOption.MINIMAL_ADDRESS_VALIDATION, "true");

            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);

            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            if (label.Success) Console.WriteLine(label.APIResponse.ParcelTrackingNumber);

            // Transaction report

            TransactionsReport<Transaction> report = new TransactionsReport<Transaction>();

            var query = from transaction in report
                        where transaction.TransactionDateTime > DateTimeOffset.Now
                        select new { transaction.MerchantId };

            foreach (var obj in report)
                Console.WriteLine(obj);
        }

        static void CreateShipmentModel()
        {
            var shipment = new CreateShipmentRequest<Shipment>() { ToAddress = new Address() };
            shipment.ToAddress.AddAddressLine("101 Jenkins Pl");
            shipment.ToAddress.CityTown = "Santa Clara";
            shipment.ToAddress.PostalCode = "95051";
            shipment.ToAddress.StateProvince = "CA";
            shipment.ToAddress.CountryCode = "US";
            shipment.FromAddress = new Address();
            shipment.FromAddress.AddAddressLine("12 Fl");
            shipment.FromAddress.AddAddressLine("3000 Summer St");
            shipment.FromAddress.CityTown = "Stamford";
            shipment.FromAddress.PostalCode = "01235";
            shipment.FromAddress.StateProvince = "CT";
            shipment.FromAddress.CountryCode = "US";
#pragma warning disable IDE0017 // Simplify object initialization
            shipment.Parcel = new Parcel();
#pragma warning restore IDE0017 // Simplify object initialization
            shipment.Parcel.Weight = new ParcelWeight { Weight = 16.0M, UnitOfMeasurement = UnitOfWeight.OZ };
            shipment.AddShipmentOptions( new ShipmentOptions() { ShipmentOption = ShipmentOption.MINIMAL_ADDRESS_VALIDATION, Value = "true"  });
            shipment.AddShipmentOptions(new ShipmentOptions() { ShipmentOption = ShipmentOption.SHIPPER_ID, Value = "9014888410" });
            shipment.AddShipmentOptions(new ShipmentOptions() { ShipmentOption = ShipmentOption.ADD_TO_MANIFEST, Value = "true" });
            shipment.AddRates(new Rates() {
                Carrier = Carrier.USPS,
                ParcelType = USPSParcelType.PKG,
                ServiceId = USPSServices.PM,
            }).AddSpecialservices(new SpecialServices() { SpecialServiceId = USPSSpecialServiceCodes.DelCon}).AddParameter(new Parameter("INPUT_VALUE", "0"));
            shipment.AddDocument( new Document() { FileFormat = FileFormat.PDF, Size = Size.DOC_8X11, PrintDialogOption = PrintDialogOption.NO_PRINT_DIALOG, Type = DocumentType.SHIPPING_LABEL });
            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);

            var label = ShipmentsMethods.CreateShipment<Shipment>(shipment).GetAwaiter().GetResult();

            Console.WriteLine(label.APIResponse.ParcelTrackingNumber);

        }

        static void ExtensionMethod()
        {
            Address address = AddressFluent<Address>.Create().USParse("101 Jenkins Pl, Santa Clara, CA 95051")
                .Status(AddressStatus.NOT_CHANGED);
        }



    }
}
