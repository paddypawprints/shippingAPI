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

using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    /// <summary>
    ///         /// File Format Content type   Size       Label Type
    /// PDF         URL            DOC_8X11   Domestic
    ///                            DOC_4X6    APO/FPO
    ///                            DOC_6X4    International(CN22, CP72-A, CP72-B)
    /// PNG         BASE64         DOC_8X11   Domestic
    ///                            DOC_4X6    APO/FPO
    ///                            DOC_6X4    International(CN22, CP72-A, CP72-B)
    /// ZPL2        BASE64         DOC_4x6    Domestic
    ///                            DOC_6X4    APO/FPO
    ///                                       International(CN22, CP72-A, CP72-B)
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        virtual public DocumentType Type { get; set;}
        /// <summary>
        /// Recommended document/label size. Actual document/label size depends on
        /// the carrier.
        /// </summary>
        /// <value>The size.</value>
        virtual public Size Size { get; set;}
        /// <summary>
        /// Gets or sets the file format.
        /// </summary>
        /// <value>The file format.</value>
        virtual public FileFormat FileFormat { get;set;}
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        virtual public ContentType ContentType {get;set;}
        /// <summary>
        /// *PDF documents only*. This defines an option to embed script that can
        /// render an interactive print dialog box for the end user within the
        /// shipment document/label.        /// </summary>
        /// <value>The print dialog option.</value>
        virtual public PrintDialogOption PrintDialogOption {get;set;}
        /// <summary>
        /// When ``contentType`` is ``URL``, this is the URL to access the shipment
        /// label or manifest.
        /// </summary>
        /// <value>The contents.</value>
        virtual public string Contents {get;set;}
        /// <summary>
        /// When ``contentType`` is ``BASE64``, this field value will have the encoded
        /// base64 string of the shipment label.This structure will repeat if
        /// there are multiple pages.
        /// </summary>
        /// <value>The pages.</value>
        virtual public IEnumerable<IPage> Pages { get; set; }
        virtual public void AddPage(IPage s)
        {
            ModelHelper.AddToEnumerable<IPage, Page>(s, () => Pages, (x) => Pages = x);
        }
    }

}