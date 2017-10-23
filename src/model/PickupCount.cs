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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class PickupCount : IPickupCount
    {
        /// <summary>
        /// Gets or sets the service identifier. Note this is a subset of the full set of services.
        /// </summary>
        /// <value>The service identifier.</value>
        virtual public PickupService ServiceId{get; set;}
        /// <summary>
        /// Gets or sets the count. The number of parcels for each service type requested. This field
        /// is used only in the request.It is not returned in the response.
        /// </summary>
        /// <value>The count.</value>
        virtual public int Count{get; set;}
        /// <summary>
        /// Gets or sets the total weight.
        /// </summary>
        /// <value>The total weight.</value>
        virtual public IParcelWeight TotalWeight{get; set;}
    }
}
