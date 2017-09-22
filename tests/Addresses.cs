using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Method;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

using static PitneyBowes.Developer.ShippingApi.SessionDefaults;

namespace tests
{
    public class Addresses
    {
        static private bool _initialized = false;
        static void Initialize()
        {
            // Initialize framework
            if (_initialized) return;
            _initialized = true;
            Init();
            Model.RegisterSerializationTypes(DefaultSession);

            // Configuration
            DefaultSession = "sandbox";
            DefaultSession.Requestor = new ShippingAPIMock();
            DefaultSession.RecordPath = string.Format("..{0}..{0}..{0}testData", Path.DirectorySeparatorChar);
            DefaultSession.AuthToken = new Token();
            DefaultSession.AuthToken.TokenType = "Bearer";
            DefaultSession.AuthToken.AccessToken = "1234567-";

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
        [Fact]
        public void HappyPath()
        {
            // $(SolutionDir)\tests\testData\shippingservices\v1\addresses\verify\1234567828607.http
            Initialize();
            var warnings = new List<string>();
            DefaultSession.LogWarning = (s) => warnings.Add(s);
            var errors = new List<string>();
            DefaultSession.LogError = (s) => errors.Add(s);

            var company = "ABC Company";
            var name = "Rufous Sirius Canid";
            var phone = "323 555-1212";
            var email = "rs.canid@gmail.com";
            var residential = false;
            var address1 = "643 Greenway RD";
            var cityTown = "Boone";
            var stateProvince = "NC";
            var postalCode = "28607";
            var countryCode = "US";

            var addressFluent = AddressFluent<Address>.Create().Company(company)
                .Person(name, phone, email)
                .Residential(residential)
                .AddressLines(address1)
                .CityTown(cityTown)
                .StateProvince(stateProvince)
                .PostalCode(postalCode)
                .CountryCode(countryCode); // calls the service for address validation
            var address = (Address)addressFluent;
            Assert.True( address != null );
            Assert.True(address.Company.Equals(company));
            Assert.True(address.Name.Equals(name));
            Assert.True(address.Phone.Equals(phone));
            Assert.True(address.Email.Equals(email));
            Assert.True(!address.Residential);
            int i = 0;
            for( var a = address.AddressLines.GetEnumerator(); a.MoveNext();)
            {
                if (i == 0) Assert.True(a.Current.Equals(address1));
                i++;
            }
            Assert.True(i == 1);
            Assert.True(address.CityTown.Equals(cityTown));
            Assert.True(address.StateProvince.Equals(stateProvince));
            Assert.True(address.PostalCode.Equals(postalCode));
            Assert.True(address.CountryCode.Equals(countryCode));
            Assert.True(address.Status == AddressStatus.NOT_CHANGED);

            address = (Address)addressFluent.Verify();
            Assert.True(address.Status == AddressStatus.VALIDATED_CHANGED);
            Assert.True(address.PostalCode.Equals("28607-4819"));
            

        }
    }
}
