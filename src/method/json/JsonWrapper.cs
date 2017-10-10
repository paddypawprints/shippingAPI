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
using System.Reflection;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    public abstract class JsonWrapper<T> where T: new()
    {
        public JsonWrapper()
        {
            Wrapped = new T();
        }

        public JsonWrapper( T t)
        {
            Wrapped = t;
        }

        public static explicit operator T(JsonWrapper<T> j)
        {
            return j.Wrapped;
        }

        public virtual T Wrapped { get; set; }

     }
}
