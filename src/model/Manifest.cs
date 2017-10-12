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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Manifest : IManifest
    {
        public Manifest()
        {
            //ParcelTrackingNumbers = new List<string>();
            //Parameters = new List<Parameter>();
            //Documents = new List<Document>();
        }
        virtual public string TransactionId { get; set; }
        virtual public Carrier Carrier { get;set;}
        virtual public DateTimeOffset SubmissionDate {get;set;}
        virtual public IAddress FromAddress {get;set;}
        virtual public string InductionPostalCode {get;set;}
        virtual public string ManifestId {get;set;}
        virtual public string ManifestTrackingNumber {get;set;}
        virtual public IEnumerable<string> ParcelTrackingNumbers { get; set; }
        virtual public IEnumerable<IParameter> Parameters { get; set; }
        virtual public IEnumerable<IDocument> Documents { get; set; }

        virtual  public IDocument AddDocument(IDocument d)
        {
            return ModelHelper.AddToEnumerable<IDocument, Document>(d, () => Documents, (v) => Documents = v );
        }

        public IParameter AddParameter(IParameter p)
        {
            return ModelHelper.AddToEnumerable<IParameter, Parameter>(p, () => Parameters, (v) => Parameters = v);
        }

        public void AddParcelTrackingNumber(string t)
        {
            ModelHelper.AddToEnumerable<string, string>(t, () => ParcelTrackingNumbers, (v) => ParcelTrackingNumbers = v);
        }
    }
}