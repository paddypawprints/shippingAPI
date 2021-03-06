﻿/*
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

namespace PitneyBowes.Developer.ShippingApi
{
    public interface  IPickupCount
    {
        /// <summary>
        /// Gets or sets the service identifier. Not that this is a subset of the full set of services.
        /// </summary>
        /// <value>The service identifier.</value>
        PickupService ServiceId { get; set; }
        /// <summary>
        /// Gets or sets the count. The number of parcels for each service type requested. This field
        /// is used only in the request.It is not returned in the response.
        /// </summary>
        /// <value>The count.</value>
        int Count { get; set; }
        /// <summary>
        /// Gets or sets the total weight.
        /// </summary>
        /// <value>The total weight.</value>
        IParcelWeight TotalWeight { get; set; }
    }

    public static partial class InterfaceExtensions
    {
        public static bool IsValid(this IPickupCount c)
        {
            return c.Count > 0;
        }
    }
}


