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

namespace PitneyBowes.Developer.ShippingApi
{
    internal class TransactionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(TransactionType).Equals(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "POSTAGE FUND":
                    return TransactionType.POSTAGE_FUND;
                case "POSTAGE PRINT":
                    return TransactionType.POSTAGE_PRINT;
                case "POSTAGE REFUND":
                    return TransactionType.POSTAQGE_REFUND;
                default:
                    var converter = new StringEnumConverter();
                    return converter.ReadJson(reader, objectType, existingValue, serializer);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!typeof(TransactionType).Equals(value.GetType())) throw new InvalidOperationException(string.Format("Cant use TransactionTypeConverter to serialize a {0}", value.GetType().ToString())); 
            var s = (TransactionType)value;
            switch (s)
            {
                case TransactionType.POSTAGE_FUND:
                    writer.WriteValue("POSTAGE FUND");
                    break;
                case TransactionType.POSTAGE_PRINT:
                    writer.WriteValue("POSTAGE PRINT");
                    break;
                case TransactionType.POSTAQGE_REFUND:
                    writer.WriteValue("POSTAGE REFUND");
                    break;
                default:
                    var converter = new StringEnumConverter();
                    converter.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }

}