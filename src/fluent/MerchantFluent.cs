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
using System.Text;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    class MerchantFluent<T> where T : IMerchant, new()
    {
        private T _merchant;

        public static MerchantFluent<T> Create()
        {
            var p = new MerchantFluent<T>() { _merchant = new T() };
            return p;
        }

        private MerchantFluent()
        {

        }

        public MerchantFluent(T m)
        {
            _merchant = m;
        }

        public static implicit operator T(MerchantFluent<T> m)
        {
            return (T)m._merchant;
        }

        public MerchantFluent<T> RegisterIFrame(string developerId, IAddress contact, IEnumerable<IPaymentInfo> paymentInfo, decimal initialPostageValue, decimal refillAmount, decimal thresholdAmount)
        {
            MerchantSignupRequest request = new MerchantSignupRequest()
            {
                DeveloperId = developerId,
                Contact = contact,
                PaymentInfo = paymentInfo,
                IntialPostageValue = initialPostageValue,
                RefillAmount = refillAmount,
                ThresholdAmount = thresholdAmount
            };
            var response = MerchantMethods.Signup<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                return new MerchantFluent<T>(response.APIResponse);
            }
            return null;
        }

        public MerchantFluent<T> RegisterEmailLink(string developerId, string username, StringBuilder password)
        {
            var request = new MerchantCredentialsRequest()
            {
                DeveloperId = developerId,
                UserName = username,
                Password = password
            };

            var response = MerchantMethods.Credentials<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                return new MerchantFluent<T>(response.APIResponse);
            }
            return null;
        }

        public MerchantFluent<T> RegisterBulkAccount(string developerId, string username, string password, IAddress address)
        {
            var request = new MerchantRegisterRequest()
            {

                DeveloperId = developerId,
                UserName = username,
                Password = password,
                AddressLines = address.AddressLines,
                CityTown = address.CityTown,
                StateProvince = address.StateProvince,
                PostalCode = address.PostalCode,
                CountryCode = address.CountryCode,
                Company = address.Company,
                Name = address.Name,
                Phone = address.Phone,
                Email = address.Email,
                Residential = address.Residential
            };

            var response = MerchantMethods.Register<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                return new MerchantFluent<T>(response.APIResponse);
            }
            return null;

        }

        public MerchantFluent<T> FullName( string f)
        {
            _merchant.FullName = f;
            return this;
        }
        public MerchantFluent<T> Email( string e)
        {
            _merchant.Email = e;
            return this;
        }
        public MerchantFluent<T> RegisteredDate(DateTimeOffset d)
        {
            _merchant.RegisteredDate = d;
            return this;
        }
        public MerchantFluent<T> PaymentAccountNumber(string p)
        {
            _merchant.PaymentAccountNumber = p;
            return this;
        }
        public MerchantFluent<T> EnterpriseAccount(string e)
        {
            _merchant.EnterpriseAccount = e;
            return this;
        }
        public MerchantFluent<T> SubscriptionAccount( string s)
        {
            _merchant.SubscriptionAccount = s;
            return this;
        }
        public MerchantFluent<T> PostalReportingNumber( string p)
        {
            _merchant.PostalReportingNumber = p;
            return this;
        }
        public MerchantFluent<T> MerchantStatus( string m)
        {
            _merchant.MerchantStatus = m;
            return this;
        }
        public MerchantFluent<T>  MerchantStatusReason( string m)
        {
            _merchant.MerchantStatusReason = m;
            return this;
        }
        public MerchantFluent<T> DeactivatedDate( DateTimeOffset d)
        {
            _merchant.DeactivatedDate = d;
            return this;
        }
    }
}
