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

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Parameter : IParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PitneyBowes.Developer.ShippingApi.Model.Parameter"/> class.
        /// </summary>
        public Parameter()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PitneyBowes.Developer.ShippingApi.Model.Parameter"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        public Parameter( string name, string value)
        {
            Name = name;
            Value = value;
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        virtual public string Name {get;set;}
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        virtual public string Value {get;set;}
    }

}