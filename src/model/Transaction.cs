/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Transaction : ITransaction
    {
        virtual public string TransactionId { get; set; }
        virtual public DateTimeOffset TransactionDateTime { get; set; }
        virtual public TransactionType TransactionType { get; set; }
        virtual public string DeveloperName { get; set; }
        virtual public string DeveloperId { get; set; }
        virtual public string DeveloperPostagePaymentMethod { get; set; }
        virtual public string DeveloperRatePlan { get; set; }
        virtual public Decimal? DeveloperRateAmount { get; set; }
        virtual public Decimal? DeveloperPostagePaymentAccountBalance { get; set; }
        virtual public string MerchantName { get; set; }
        virtual public string MerchantId { get; set; }
        virtual public string MerchantPostageAccountPaymentMethod { get; set; }
        virtual public string MerchantRatePlan { get; set; }
        virtual public Decimal? MerchantRate { get; set; }
        virtual public Decimal? ShipperPostagePaymentAccountBalance { get; set; }
        virtual public Decimal? LabelFee { get; set; }
        virtual public string ParcelTrackingNumber { get; set; }
        virtual public Decimal? WeightInOunces { get; set; }
        virtual public int? Zone { get; set; }
        virtual public Decimal? PackageLengthInInches { get; set; }
        virtual public Decimal? PackageWidthInInches { get; set; }
        virtual public Decimal? PackageHeightInInches { get; set; }
        virtual public PackageTypeIndicator? PackageTypeIndicator { get; set; }
        virtual public ParcelType? PackageType { get; set; }
        virtual public string MailClass { get; set; }
        virtual public string InternationalCountryPriceGroup { get; set; }
        virtual public string OriginationAddress { get; set; }
        virtual public string OriginZip { get; set; }
        virtual public string DestinationAddress { get; set; }
        virtual public string DestinationZip { get; set; }
        virtual public string DestinationCountry { get; set; }
        virtual public Decimal? PostageDepositAmount { get; set; }
        virtual public Decimal? CreditCardFee { get; set; }
        virtual public string RefundStatus { get; set; }
        virtual public string RefundDenialReason { get; set; }
    }
}
