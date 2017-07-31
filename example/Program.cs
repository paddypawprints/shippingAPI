using System;
using System.Collections.Generic;
using com.pb.shippingapi;
using com.pb.shippingapi.model;
using com.pb.shippingapi.fluent;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            ShippingApi.Init();
            ShippingApi.DefaultSession = "sandbox";
            ShippingApi.DefaultSession.LogWarning = (s)=> Console.WriteLine("Warning:" + s);
            ShippingApi.DefaultSession.LogError = (s)=> Console.WriteLine("Error:" + s);
            ShippingApi.DefaultSession.LogConfigError = (s)=> Console.WriteLine("Bad code:" + s);
            ShippingApi.DefaultSession.GetAPISecret = ()=> "wgNEtZkNbP0iV8h0".ToCharArray();
            ShippingApi.AddConfigItem("ApiKey", "Ci4vEAgBP8Aww7TBwGOKhr43uKTPNyfO");
            ShippingApi.AddConfigItem("RatePlan", "PP_SRP_CBP");


            var tokenResponse = TokenMethods.token().GetAwaiter().GetResult();

            CreateShipmentModel();
            CreateShipmentFluent();
        }

        static void CreateShipmentModel()
        {

            var shipment = new CreateShipmentRequest();

            shipment.ToAddress = new Address();
            shipment.ToAddress.AddressLines = new string[] {"101 Jenkins Pl"};
            shipment.ToAddress.CityTown = "Santa Clara";
            shipment.ToAddress.PostalCode = "95051";
            shipment.ToAddress.StateProvince = "CA";
            shipment.ToAddress.CountryCode = "US";
            shipment.FromAddress = new Address();
            shipment.FromAddress.AddressLines = new string[] {"12 Fl", "3000 Summer St"};
            shipment.FromAddress.CityTown = "Stamford";
            shipment.FromAddress.PostalCode = "01235";
            shipment.FromAddress.StateProvince = "CT";
            shipment.FromAddress.CountryCode = "US";
            shipment.Parcel = new Parcel();
            shipment.Parcel.Weight = new ParcelWeight();
            shipment.Parcel.Weight.Weight = 16.0M;
            shipment.Parcel.Weight.UnitOfMeasurement = UnitOfWeight.OZ;
            shipment.ShipmentOptions = new ShipmentOptions[3];
            var options = (ShipmentOptions[])shipment.ShipmentOptions;
            options[0] = new ShipmentOptions();
            options[0].ShipmentOption = ShipmentOption.MINIMAL_ADDRESS_VALIDATION;
            options[0].Value = "true";
            options[1] = new ShipmentOptions();
            options[1].ShipmentOption = ShipmentOption.ADD_TO_MANIFEST;
            options[1].Value = "true";
            options[2] = new ShipmentOptions();
            options[2].ShipmentOption = ShipmentOption.SHIPPER_ID;
            options[2].Value = "9014888410";
            shipment.Rates = new Rates[1];
            var rate = new Rates();
            ((Rates[])shipment.Rates)[0] = rate;
            rate.Carrier = Carrier.usps;
            rate.ParcelType = USPSParcelType.PKG;
            rate.serviceId = USPSServices.PM;
            rate.specialServices = new SpecialServices[1];
            var services = (SpecialServices[])rate.specialServices;
            services[0] = new SpecialServices();
            services[0].SpecialServiceId =  USPSSpecialServiceCodes.DelCon;
            services[0].InputParameters = new Parameter[1];
            var parameters = (Parameter[])services[0].InputParameters;
            parameters[0] = new Parameter();
            parameters[0].Name = "INPUT_VALUE";
            parameters[0].Value = "0";
            shipment.Documents = new Document[1];
            ((Document[])shipment.Documents)[0] = new Document();
            var document = ((Document[])shipment.Documents)[0];
            document.FileFormat = FileFormat.PDF;
            document.Size = Size.DOC_8X11;
            document.printDialogOption = PrintDialogOption.NO_PRINT_DIALOG;
            document.Type = DocumentType.SHIPPING_LABEL;


            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);
            
            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            Console.WriteLine(label.APIResponse.ParcelTrackingNumber);
            
        }

        static void CreateShipmentFluent()
        {
            var shipment = new CreateShipmentRequest();
            shipment.ToAddress = AddressFluent.Create()
                .AddressLines("101 Jenkins Place")
                .CityTown("Santa Clara")
                .StateProvince("CA")
                .PostalCode("95051")
                .CountryCode("US");
            shipment.FromAddress = AddressFluent.Create()
                .AddressLines("12FL", "3000 Summer St.")
                .CityTown("Stamford")
                .StateProvince("CT")
                .PostalCode("05051")
                .CountryCode("US");
            shipment.Parcel = ParcelFluent.Create()
                .Weight(16m, UnitOfWeight.OZ);
            shipment.Rates = RatesArrayFluent.Create().Add()
                .Carrier(Carrier.usps)
                .ParcelType(USPSParcelType.PKG)
                .Service(USPSServices.PM)
                .specialService(USPSSpecialServiceCodes.DelCon, 0M, new Parameter(){Name="INPUT_VALUE", Value = "0"});
            shipment.Documents = DocumentsArrayFluent.Create().Add()
                .DocumentType(DocumentType.SHIPPING_LABEL)
                .ContentType(ContentType.URL)
                .Size(Size.DOC_8X11)
                .FileFormat(FileFormat.PDF)
                .PrintDialogOption(PrintDialogOption.EMBED_PRINT_DIALOG);
            shipment.ShipmentOptions = ShipmentOptionsArrayFluent.Create()
                .Add().Option(ShipmentOption.SHIPPER_ID, "9014888410")
                .Add().Option(ShipmentOption.ADD_TO_MANIFEST, "true")
                .Add().Option(ShipmentOption.MINIMAL_ADDRESS_VALIDATION, "true");

            shipment.TransactionId = Guid.NewGuid().ToString().Substring(15);

            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            Console.WriteLine(label.APIResponse.ParcelTrackingNumber);
        }

    }
}
