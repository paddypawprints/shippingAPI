
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

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// Fluent class to set up a document array for shipments.
    /// </summary>
    public class DocumentsArrayFluent<T> where T : IDocument, new()
    {
        /// <summary>
        /// Factory method to create an instance - use instead of new to start the method chain.
        /// </summary>
        /// <returns>The create.</returns>
        public static DocumentsArrayFluent<T> Create()
        {
            return new DocumentsArrayFluent<T>();
        }
        public static implicit operator List<T>(DocumentsArrayFluent<T> d) => d._list;

        protected List<T> _list = new List<T>();
        protected T _current = default(T);
        /// <summary>
        /// Add a new document to the end of the list. Current will point to it.
        /// </summary>
        /// <returns>this</returns>
        public DocumentsArrayFluent<T> Add() 
        {
            _list.Add(new T());
            _current = _list.FindLast((x) => true);
            return this;
        }
        /// <summary>
        /// Reposition the pointer to the first document in the list. 
        /// </summary>
        /// <returns>The first.</returns>
        public DocumentsArrayFluent<T> First()
        {
            _current = _list.Find((x) => true);
            return this;
        }
        /// <summary>
        /// Move Current to the next item in the list. 
        /// </summary>
        /// <returns>The next.</returns>
        public DocumentsArrayFluent<T> Next()
        {
            var i = _list.IndexOf(_current);
            _current = _list[i + 1];
            return this;
        }
        /// <summary>
        /// Is this the lst item in the list.
        /// </summary>
        /// <returns><c>true</c>, if last was ised, <c>false</c> otherwise.</returns>
        public bool IsLast()
        {
            var i = _list.IndexOf(_current);
            return (i == _list.Count - 1);
        }
        /// <summary>
        /// Gets or sets the current item.
        /// </summary>
        /// <value>The current item.</value>
        public T Current { get => _current;  set { _current = value; }}

        /// <summary>
        /// Sets the type of the current item.
        /// </summary>
        /// <returns>The type.</returns>
        /// <param name="type">Type.</param>
        public DocumentsArrayFluent<T> DocumentType(DocumentType type ) 
        {
            _current.Type = type;
            return this;
        }
        /// <summary>
        /// Set the size of the current item. Recommended document/label size. Actual document/label size depends on
        /// the carrier.
        /// </summary>
        /// <returns>this</returns>
        /// <param name="size">Size.</param>
        public DocumentsArrayFluent<T> Size( Size size)
        {
            _current.Size = size;
            return this;
        }
        /// <summary>
        /// Sets the file format of the current itewm.
        /// </summary>
        /// <returns>The format.</returns>
        /// <param name="format">Format.</param>
        public DocumentsArrayFluent<T> FileFormat(FileFormat format )
        {
            _current.FileFormat = format;
            return this;
        }
        /// <summary>
        /// Sets the content type of the current item..
        /// </summary>
        /// <returns>The type.</returns>
        /// <param name="content">Content.</param>
        public DocumentsArrayFluent<T> ContentType(ContentType content)
        {
            _current.ContentType = content;
            return this;
        }
        /// <summary>
        /// *PDF documents only*. This defines an option to embed script that can
        /// render an interactive print dialog box for the end user within the
        /// shipment document/label.
        /// </summary>
        /// <returns>this</returns>
        /// <param name="option">Option.</param>
        public DocumentsArrayFluent<T> PrintDialogOption(PrintDialogOption option)
        {
            _current.PrintDialogOption = option;
            return this;
        }
        /// <summary>
        /// Set document defaults for a shipping label.
        /// </summary>
        /// <returns>this</returns>
        /// <param name="contentType">Content type.</param>
        /// <param name="size">Size.</param>
        /// <param name="fileFormat">File format.</param>
        public DocumentsArrayFluent<T> ShippingLabel( ContentType contentType = ShippingApi.ContentType.URL, Size size = ShippingApi.Size.DOC_8X11, FileFormat fileFormat = ShippingApi.FileFormat.PDF)
        {
            return Add()
                .DocumentType(ShippingApi.DocumentType.SHIPPING_LABEL)
                .ContentType(contentType)
                .Size(size)
                .FileFormat(fileFormat)
                .PrintDialogOption(ShippingApi.PrintDialogOption.NO_PRINT_DIALOG);
        }
    }
}