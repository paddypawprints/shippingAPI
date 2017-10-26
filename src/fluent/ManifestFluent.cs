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
    public class ManifestFluent<T> where T : IManifest, new()
    {
        private T _manifest;

        public static ManifestFluent<T> Create()
        {
            var p = new ManifestFluent<T>() { _manifest = new T() };
            return p;
        }

        private ManifestFluent()
        {

        }
        public static implicit operator T(ManifestFluent<T> m)
        {
            return (T)m._manifest;
        }

        public IManifest Manifest 
        {
            get => _manifest;
            set => _manifest = (T)value;
        }

        public ManifestFluent<T> Submit(ISession session = null)
        {
            if (session == null)
            {
                session = Globals.DefaultSession;
            }

            var response = ManifestMethods.Create(_manifest, session).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            else
            {
                throw new ShippingAPIException(response);
            }

            return this;
        }

        public ManifestFluent<T> Reprint(string manifestId, ISession session = null)
        {
            if (session == null)
            {
                session = Globals.DefaultSession;
            }

            var request = new ReprintManifestRequest() { ManifestId = manifestId };
            var response = ManifestMethods.Reprint<T>(request, session).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            else
            {
                throw new ShippingAPIException(response);
            }

            return this;
        }

        public ManifestFluent<T> Retry(string originalId, ISession session = null)
        {
            if (session == null)
            {
                session = Globals.DefaultSession;
            }

            var request = new RetryManifestRequest() { OriginalTransactionId = originalId };
            var response = ManifestMethods.Retry<T>(request).GetAwaiter().GetResult();
            if (response.Success)
            {
                _manifest = response.APIResponse;
            }
            else
            {
                throw new ShippingAPIException(response);
            }

            return this;
        }

        public ManifestFluent<T> Carrier(Carrier c)
        {
            _manifest.Carrier = c;
            return this;
        }
        public ManifestFluent<T> SubmissionDate(DateTime s)
        {
            _manifest.SubmissionDate = s;
            return this;
        }
        public ManifestFluent<T> FromAddress(IAddress a)
        {
            _manifest.FromAddress = a;
            return this;
        }
        public ManifestFluent<T> InductionPostalCode(string p)
        {
            _manifest.InductionPostalCode = p;
            return this;
        }
        public ManifestFluent<T> ParcelTrackingNumbers(IEnumerable<string> tl)
        {
            foreach (var t in tl)
                _manifest.AddParcelTrackingNumber(t);
            return this;
        }
        public ManifestFluent<T> AddParcelTrackingNumber(string t)
        {
            _manifest.AddParcelTrackingNumber(t);
            return this;
        }
        public ManifestFluent<T> Parameters(IEnumerable<IParameter> pl)
        {
            foreach (var p in pl)
                _manifest.AddParameter(p);
            return this;
        }
        public ManifestFluent<T> AddParameter(IParameter p)
        {
            _manifest.AddParameter(p);
            return this;
        }
        public ManifestFluent<T> AddParameter<P>(string name, string value) where P : IParameter, new()
        {
            var p = new P
            {
                Name = name,
                Value = value
            };
            _manifest.AddParameter(p);
            return this;
        }
        public ManifestFluent<T> AddParameter<P>(ManifestParameter param, string value) where P : IParameter, new()
        {
            AddParameter<P>(param.ToString(), value);
            return this;
        }
        public ManifestFluent<T> TransactionId(string t)
        {
            _manifest.TransactionId = t;
            return this;
        }

    }
}

