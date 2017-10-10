/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IDocument
    { 
        DocumentType Type { get; set; }
        Size Size { get; set; }
        FileFormat FileFormat { get; set; }
        ContentType ContentType { get; set; }
        PrintDialogOption PrintDialogOption { get; set; }
        string Contents { get; set; }
        IEnumerable<string> Pages { get; set; }
        void AddPage(string p);
    }

}