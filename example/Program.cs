using System;
using com.pb.shippingapi;
using com.pb.shippingapi.model;

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
            shipment.FromAddress = new Address();
            shipment.FromAddress.AddressLines = new string[] {"12 Fl", "3000 Summer St"};
            shipment.FromAddress.CityTown = "Stamford";
            shipment.FromAddress.PostalCode = "01235";
            shipment.FromAddress.StateProvince = "CT";
            shipment.Parcel = new Parcel();
            shipment.Parcel.Weight = new ParcelWeight();
            shipment.Parcel.Weight.Weight = 16.0M;
            shipment.Parcel.Weight.UnitOfMeasurement = UnitOfWeight.OZ;
            shipment.ShipmentOptions = new ShipmentOptions[1];
            shipment.ShipmentOptions[0] = new ShipmentOptions();
            shipment.ShipmentOptions[0].ShipmentOption = ShipmentOption.MINIMAL_ADDRESS_VALIDATION;
            shipment.Rates = new Rates();
            shipment.Rates.Carrier = Carrier.usps;
            shipment.Rates.ParcelType = USPSParcelType.PKG;
            shipment.Rates.serviceId = USPSServices.PM;
            shipment.Rates.specialServices = new SpecialServices[1];
            var services = (SpecialServices[])shipment.Rates.specialServices;
            services[0] = new SpecialServices();
            services[0].SpecialServiceId =  USPSSpecialServiceCodes.DelCon;
            services[0].InputParameters = new Parameter[1];
            var parameters = (Parameter[])services[0].InputParameters;
            parameters[0] = new Parameter();
            parameters[0].Name = "DelCon";
            parameters[0].Value = "true";
            
            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            Console.WriteLine(label.APIResponse.ParcelTrackingNumber);
            
        }

        static void CreateShipmentFluent()
        {
            var shipment = new CreateShipmentRequest();
            shipment.ToAddress = com.pb.shippingapi.fluent.Address.Create()
                .AddressLines("101 Jenkins Place")
                .CityTown("Santa Clara")
                .StateProvince("CA")
                .PostalCode("95051");
            shipment.FromAddress = com.pb.shippingapi.fluent.Address.Create()
                .AddressLines("12FL", "3000 Summer St.")
                .CityTown("Stamford")
                .StateProvince("CT")
                .PostalCode("05051");
            shipment.Parcel = com.pb.shippingapi.fluent.Parcel.Create()
                .Weight(16m).oz();
            shipment.Rates = new com.pb.shippingapi.fluent.Rates()
                .Carrier(Carrier.usps)
                .ParcelType(USPSParcelType.PKG)
                .Service(USPSServices.PM)
                .specialService(USPSSpecialServiceCodes.DelCon, 0M, new {Name="DelCon", Value = "true"});

            var label = ShipmentsMethods.CreateShipment(shipment).GetAwaiter().GetResult();

            Console.WriteLine(label.APIResponse.ParcelTrackingNumber);
        }

    }
}
