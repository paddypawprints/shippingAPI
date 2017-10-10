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
    public class Merchant : IMerchant
    {
        virtual public string FullName {get;set;}
        virtual public string Email {get;set;}
        virtual public DateTimeOffset RegisteredDate { get;set;}
        virtual public string PaymentAccountNumber {get;set;}
        virtual public string EnterpriseAccount {get;set;}
        virtual public string SubscriptionAccount {get;set;}
        virtual public string PostalReportingNumber {get;set;}
        virtual public string MerchantStatus {get;set;}
        virtual public string MerchantStatusReason {get;set;}
        virtual public DateTimeOffset DeactivatedDate {get;set;}       
    }

}
