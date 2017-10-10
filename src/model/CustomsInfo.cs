/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CustomsInfo : ICustomsInfo
    {
        virtual public ReasonForExport ReasonForExport { get; set;}
        virtual public string reasonForExportExplanation { get; set;}
        virtual public string Comments { get;set;}
        virtual public string InvoiceNumber {get;set;}
        virtual public string ImporterCustomsReference {get;set;}
        virtual public string InsuredNumber {get;set;}
        virtual public decimal InsuredAmount {get;set;}
        virtual public decimal SdrValue { get;set;}
        virtual public string EELPFC {get;set;}
        virtual public string FromCustomsReference {get;set;}
        virtual public decimal CustomsDeclaredValue { get;set;}
        virtual public string CurrencyCode {get;set;}
        virtual public string LicenseNumber {get;set;}
        virtual public string CertificateNumber {get;set;}
    }
}