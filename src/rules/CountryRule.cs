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
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    public class CountryRule
    {
        public class Country : ICountry
        {
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
        }

        private static object _lock = new object();
        public static Dictionary<string, string> Rules = new Dictionary<string, string>();
        public static DateTimeOffset? LastUpdate { get; set; }

        private static void Load(ISession session = null)
        {
            if (LastUpdate == null || DateTimeOffset.Now - LastUpdate > TimeSpan.FromHours(1))
            {
                lock (_lock)
                {
                    // Load countries
                    var countriesRequest = new CountriesRequest<Country>()
                    {
                        Carrier = Carrier.USPS,
                        OriginCountryCode = "US"
                    };
                    var countriesResponse = CountriesMethods.Countries(countriesRequest, session).GetAwaiter().GetResult();
                    if (countriesResponse.Success)
                    {
                        Rules.Clear();
                        foreach (var c in countriesResponse.APIResponse)
                        {
                            Rules[c.CountryCode] = c.CountryName;
                        }
                        LastUpdate = DateTimeOffset.Now;
                    }
                    else
                    {
                        //TODO: Unhappy
                    }
                }
            }
        }
        public static bool Validate(string countryCode, ISession session)
        {
            Load(session);
            lock (_lock)
            {
                return Rules.ContainsKey(countryCode);
            }
        }
    }
}
