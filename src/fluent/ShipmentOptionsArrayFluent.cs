
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

using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Model;


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class ShipmentOptionsArrayFluent<T> : List<T> where T : class, IShipmentOptions, new()
    {
        public static ShipmentOptionsArrayFluent<T> Create()
        {
            return new ShipmentOptionsArrayFluent<T>();
        }
        protected T _current = null;

        public ShipmentOptionsArrayFluent<T> Add() 
        {
            Add(new T());
            _current = FindLast((x) => true);
            return this;
        }

        public ShipmentOptionsArrayFluent<T> First()
        {
            _current = Find((x) => true);
            return this;
        }

        public ShipmentOptionsArrayFluent<T> Next()
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
        public ShipmentOptionsArrayFluent<T> Option(ShipmentOption option, string value ) 
        {
            _current.ShipmentOption = option;
            _current.Value = value;
            return this;
        }

        public ShipmentOptionsArrayFluent<T> ShipperId( string shipperId)
        {
            return Add().Option(ShipmentOption.SHIPPER_ID, shipperId);
        }
        public ShipmentOptionsArrayFluent<T> AddToManifest(bool value = true)
        {
            return Add().Option(ShipmentOption.ADD_TO_MANIFEST, value.ToString());
        }
        public ShipmentOptionsArrayFluent<T> MinimalAddressvalidation(bool value = true)
        {
            return Add().Option(ShipmentOption.MINIMAL_ADDRESS_VALIDATION, value.ToString());
        }
        public ShipmentOptionsArrayFluent<T> AddOption(ShipmentOption option, string value)
        {
            return Add().Option(option, value);
        }

    }
}