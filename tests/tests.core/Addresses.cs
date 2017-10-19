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

using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Fluent;
using PitneyBowes.Developer.ShippingApi.Model;
using System.Collections.Generic;
using Xunit;


namespace tests
{
    public class Addresses
    {

        [Fact]
        public void HappyPath()
        {
            // $(SolutionDir)\tests\testData\shippingservices\v1\addresses\verify\1234567828607.http
            TestSession.Initialize();
            var warnings = new List<string>();
            Globals.DefaultSession.LogWarning = (s) => warnings.Add(s);
            var errors = new List<string>();
            Globals.DefaultSession.LogError = (s) => errors.Add(s);

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
            Assert.Same(address.Company,company);
            Assert.Same(address.Name,name);
            Assert.Same(address.Phone,phone);
            Assert.Same(address.Email,email);
            Assert.True(!address.Residential);
            int i = 0;
            for( var a = address.AddressLines.GetEnumerator(); a.MoveNext();)
            {
                if (i == 0) Assert.Same(a.Current,address1);
                i++;
            }
            Assert.True(i == 1);
            Assert.Same(address.CityTown,cityTown);
            Assert.Same(address.StateProvince,stateProvince);
            Assert.Same(address.PostalCode,postalCode);
            Assert.Same(address.CountryCode,countryCode);
            Assert.True(address.Status == AddressStatus.NOT_CHANGED);

            address = (Address)addressFluent.Verify();
            Assert.True(address.Status == AddressStatus.VALIDATED_CHANGED);
            Assert.Same(address.PostalCode,"28607-4819");
            

        }
    }
}
