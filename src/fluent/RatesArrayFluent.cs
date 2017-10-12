using Newtonsoft.Json;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Model;

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

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class RatesArrayFluent<T> : List<T> where T : class, IRates, new()
    {

        protected T _current = null;

        public static RatesArrayFluent<T> Create()
        {
            return new RatesArrayFluent<T>();
        }

        public RatesArrayFluent<T> Add() 
        {
            Add(new T());
            _current = FindLast((x) => true);
            return this;
        }

        public RatesArrayFluent<T> First()
        {
            _current = Find((x) => true);
            return this;
        }

        public RatesArrayFluent<T> Next()
        {
            var i = IndexOf(_current);
            _current = this[i + 1];
            return this;
        }

        public bool IsLast()
        {
            var i = IndexOf(_current);
            return (i == Count - 1);
        }

        public RatesArrayFluent<T> Carrier(Carrier c) 
        {
            _current.Carrier = c;
            return this;
        }

        public RatesArrayFluent<T> Service(Services s) 
        {
            _current.ServiceId = s;
            return this;
        }
        public RatesArrayFluent<T> ParcelType(ParcelType t) 
        {
            _current.ParcelType = t;
            return this;
        }

        public RatesArrayFluent<T> SpecialService<S>(SpecialServiceCodes c, decimal f, params IParameter[] parameters) where S:ISpecialServices, new()
        {
            var s = new S() { SpecialServiceId = c, Fee = f };

            foreach( var p in parameters )
            {
                s.AddParameter(p);
            }
            _current.AddSpecialservices(s);
            return this;
        }

        public RatesArrayFluent<T> SpecialService<S>(S s) where S : ISpecialServices, new()
        {
            _current.AddSpecialservices(s);
            return this;
        }

        public RatesArrayFluent<T> InductionPostalCode(string s) 
        {
            _current.InductionPostalCode = s;
            return this;
        }

        public RatesArrayFluent<T> DimensionalWeight<S>(decimal w, UnitOfWeight u) where S: IParcelWeight, new()
        {
            _current.DimensionalWeight = new S(){Weight = w, UnitOfMeasurement = u};
            return this;
        }
         public RatesArrayFluent<T> DeliveryCommitment(IDeliveryCommitment c) 
         {
            _current.DeliveryCommitment = c;
             return this;
         }

        public RatesArrayFluent<T> CurrencyCode(string s) 
        {
            _current.CurrencyCode = s;
            return this;
        }
        public RatesArrayFluent<T> Insurance(decimal amount)
        {
            return SpecialService<SpecialServices>(SpecialServiceCodes.Ins, 0M, new Parameter("INPUT_VALUE", amount.ToString()));
        }
    }
}