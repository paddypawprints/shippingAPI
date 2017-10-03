using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using System.Collections.Generic;
using Xunit;

using static PitneyBowes.Developer.ShippingApi.SessionDefaults;

namespace tests
{
    public class Addresses
    {

        [Fact]
        public void HappyPath()
        {
            // $(SolutionDir)\tests\testData\shippingservices\v1\addresses\verify\1234567828607.http
            Session.Initialize();
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
