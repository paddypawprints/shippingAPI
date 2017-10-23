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
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IAutoRefill
    {
        /// <summary>
        /// The value of this field depends on whether the object is part of the
        /// request or the response:
        /// - **Request**: This field is set to the ``postalReportingNumber`` for
        ///      the merchant, as found in the :ref:`merchant object <object-merchant>`.
        /// - **Response**: This field is set to the ``paymentAccountNumber`` for
        /// the merchant, as found in the :ref:`merchant object <object-merchant>`.
        /// **Note:** The merchant's ``postalReportingNumber`` is separate from the
        /// merchant's ``paymentAccountNumber``.
        /// </summary>
        /// <value>The merchant identifier.</value>
        string MerchantID { get; set; }
        /// <summary>
        /// This field is *required* in a request if you are doing any of the following:
        /// - enabling automatic refill
        /// - updating the threshold
        /// - updating the refill amount
        /// If you do *not* include this field, the value is set to ``null``, which
        /// effectively disables automatic refill for the account.
        /// For recommended settings, see :ref:`when-does-auto-refill-trigger`.
        /// </summary>
        /// <value>The amount at which the merchant's PB Postage Account is refilled. The
        /// account refills when the balance falls below this value.</value>
        decimal Threshold { get; set; }
        /// <summary>
        /// This field is *required* in a request if you are doing any of the following:
        /// - enabling automatic refill
        /// - updating the threshold
        /// - updating the refill amount
        /// If you do *not* include this field, the value is set to ``null``, which
        /// effectively disables automatic refill for the account.
        ///
        /// For recommended settings, see :ref:`when-does-auto-refill-trigger`.
        /// </summary>
        /// <value>The amount added to the merchant's PB Postage Account when the balance
        /// falls below the ``threshold`` value.</value>
        decimal AddAmount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PitneyBowes.Developer.ShippingApi.IAutoRefill"/>
        /// is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }
    }

    public static class IAutoRefillExtensions
    {
        public static bool IsValid(this IAutoRefill a) => true;
    }
}
