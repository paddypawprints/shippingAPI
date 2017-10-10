/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonParcelDimension<T> : JsonWrapper<T>, IParcelDimension where T : IParcelDimension, new()
    {
        public JsonParcelDimension() : base() { }

        public JsonParcelDimension(T t) : base(t) { }

        public JsonParcelDimension(decimal l, decimal h, decimal w,  UnitOfDimension u = UnitOfDimension.IN) : base()
        {
            Wrapped.Length = l;
            Wrapped.Height = h;
            Wrapped.Width = w;
            Wrapped.UnitOfMeasurement = u;
            Wrapped.IrregularParcelGirth = 0.0M;
        }

        [JsonProperty(PropertyName ="length", Order = 1)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Length
        {
            get => Wrapped.Length;
            set { Wrapped.Length = value; }
        }
        [JsonProperty(PropertyName ="height", Order = 2)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Height
        {
            get => Wrapped.Height;
            set { Wrapped.Height = value; }
        }
        [JsonProperty(PropertyName ="width", Order = 3)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Width
        {
            get => Wrapped.Width;
            set { Wrapped.Width = value; }
        }
        [JsonProperty(PropertyName ="unitOfMeasurement", Order = 0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnitOfDimension UnitOfMeasurement
        {
            get => Wrapped.UnitOfMeasurement;
            set { Wrapped.UnitOfMeasurement = value; }
        }
        public bool ShouldSerializeIrregularParcelGirth() { return IrregularParcelGirth > 0.0M; }

        [JsonProperty(PropertyName ="irregularParcelGirth", Order = 4)]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal IrregularParcelGirth
        {
            get => Wrapped.IrregularParcelGirth;
            set { Wrapped.IrregularParcelGirth = value; }
        }

    }
}