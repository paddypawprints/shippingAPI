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
    [CodeGeneration( GenerateJsonWrapper = true, GenerateModel = false)]
    public interface ITransaction
    {
        string TransactionId {get;set;}
        DateTimeOffset TransactionDateTime { get; set; }
        TransactionType TransactionType { get; set; }
        string DeveloperName { get; set; }
        string DeveloperId { get; set; }
        string DeveloperPostagePaymentMethod { get; set; }
        string DeveloperRatePlan { get; set; }
        decimal? DeveloperRateAmount { get; set; }
        decimal? DeveloperPostagePaymentAccountBalance { get; set; }
        string MerchantName { get; set; }
        string MerchantId { get; set; }
        string MerchantPostageAccountPaymentMethod { get; set; }
        string MerchantRatePlan { get; set; }
        decimal? MerchantRate { get; set; }
        decimal? ShipperPostagePaymentAccountBalance { get; set; }
        decimal? LabelFee { get; set; }
        string ParcelTrackingNumber { get; set; }
        decimal? WeightInOunces { get; set; }
        int? Zone { get; set; }
        decimal? PackageLengthInInches { get; set; }
        decimal? PackageWidthInInches { get; set; }
        decimal? PackageHeightInInches { get; set; }
        PackageTypeIndicator? PackageTypeIndicator { get; set; }
        ParcelType? PackageType { get; set; }
        string MailClass { get; set; }
        string InternationalCountryPriceGroup { get; set; }
        string OriginationAddress { get; set; }
        string OriginZip { get; set; }
        string DestinationAddress { get; set; }
        string DestinationZip { get; set; }
        string DestinationCountry { get; set; }
        decimal? PostageDepositAmount { get; set; }
        decimal? CreditCardFee { get; set; }
        string RefundStatus { get; set; }
        string RefundDenialReason { get; set; }
    }
}
