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
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    /// <summary>
    /// Street address and/or apartment and/or P.O. Box. You can specify up to three address lines.
    /// </summary>
    public class  Address : IAddress
    {
        public Address()
        {
            Status = AddressStatus.NOT_CHANGED;
        }
        virtual public IEnumerable<string> AddressLines { get; set; }
        virtual public void AddAddressLine(string s)
        {
            if (AddressLines as List<string> != null )
            {
                if ((AddressLines as List<string>).Count == 3) throw new InvalidOperationException("Address can only have 3 lines");
            }
            ModelHelper.AddToEnumerable<string, string>(s,()=>AddressLines, (x)=>AddressLines = x);
        }
        virtual public string CityTown { get; set;}
        virtual public string StateProvince { get;set;}
        virtual public string PostalCode {get;set;}
        virtual public string CountryCode {get;set;}
        virtual public string Company {get;set;}
        virtual public string Name {get;set;}
        virtual public string Phone { get;set;}
        virtual public string Email {get;set;}
        virtual public bool Residential {get;set;}
        virtual public AddressStatus Status { get;set;}
    }
}