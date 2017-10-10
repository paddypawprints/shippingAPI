﻿/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingApiAttribute : Attribute
    {
        public string Name { get;set; }
        public string Format { get; set; }
        public ShippingApiAttribute(string name)
        {
            Name = name;
        }

    }

    public class ShippingApiHeaderAttribute : ShippingApiAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingApiHeaderAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }
    public class ShippingApiQueryAttribute : ShippingApiAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingApiQueryAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }

    public class ShippingApiResourceAttribute : ShippingApiAttribute
    {
        public bool AddId { get; set; }
        public string PathSuffix { get; set; }
        public ShippingApiResourceAttribute(string name, bool addId = true, string pathSuffix = null) : base(name)
        {
            AddId = addId;
            PathSuffix = PathSuffix;
        }

    }
}