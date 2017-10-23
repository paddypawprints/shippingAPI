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
using PitneyBowes.Developer.ShippingApi.Method;


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// An address. If part of a response, this object also specifies address validation status, unless minimum validation is enabled.
    /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
    /// </summary>
    public class PickupFluent<T> where T : IPickup, new()
    {
        private T _pickup;

        public static implicit operator T(PickupFluent<T> a)
        {
            return a._pickup;
        }

        public static PickupFluent<T> Create()
        {
            var a = new PickupFluent<T>()
            {
                _pickup = new T()
            };
            return a;
        }

        public PickupFluent()
        {
            _pickup = new T();
        }

        public PickupFluent(IAddress a)
        {
            _pickup = (T)a;
        }

        public PickupFluent<T> TransactionId(string id)
        {
            _pickup.TransactionId = id;
            return this;
        }

        public PickupFluent<T> Schedule(Session session = null)
        {
            var response = PickupMethods.Schedule<T>(_pickup, session).GetAwaiter().GetResult();
            _pickup = response.APIResponse;
            return this;
        }

        public string Cancel(Session session = null)
        {
            var cancel = new PickupCancelRequest()
            {
                PickupId = _pickup.PickupId
            };

            var response = PickupMethods.CancelPickup(cancel, session).GetAwaiter().GetResult();
            var status = response.APIResponse.Status;
            return status;
        }


        public PickupFluent<T> PickupAddress( IAddress a)
        {
            _pickup.PickupAddress = a;
            return this;
        }
        public PickupFluent<T> Carrier( Carrier c)
        {
            _pickup.Carrier = c;
            return this;
        }
        public PickupFluent<T> PickupSummary(IEnumerable<IPickupCount> s)
        {
            foreach( var p in s)
            {
                _pickup.AddPickupCount(p);
            }
            return this;
        }
        public PickupFluent<T> AddPickupSummary(IPickupCount s)
        {
            _pickup.AddPickupCount(s);
            return this;
        }

        public PickupFluent<T> AddPickupSummary<C,W>(PickupService s, int c, decimal w, UnitOfWeight u ) where C : IPickupCount, new() where W: IParcelWeight, new()
        {
            var ct = new C
            {
                ServiceId = s,
                TotalWeight = new W { UnitOfMeasurement = u, Weight = w },
                Count = c
            };
            _pickup.AddPickupCount(ct);
            return this;
        }


        public PickupFluent<T> Reference(string s)
        {
            _pickup.Reference = s;
            return this;
        }
        public PickupFluent<T> PackageLocation(PackageLocation p)
        {
            _pickup.PackageLocation = p;
            return this;
        }
        public PickupFluent<T> SpecialInstructions(string s)
        {
            _pickup.SpecialInstructions = s;
            return this;
        }
        public PickupFluent<T> PickupDate( DateTime d)
        {
            _pickup.PickupDate = d;
            return this;
        }
        public PickupFluent<T> PickupConfirmationNumber( string p)
        {
            _pickup.PickupConfirmationNumber = p;
            return this;
        }
        public PickupFluent<T> PickupId(string p)
        {
            _pickup.PickupId = p;
            return this;
        }
    }
}
