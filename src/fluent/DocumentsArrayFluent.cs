
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
    public class DocumentsArrayFluent<T> : List<IDocument> where T : IDocument, new()
    {
        public static DocumentsArrayFluent<T> Create()
        {
            return new DocumentsArrayFluent<T>();
        }
        protected IDocument _current = null;

        public DocumentsArrayFluent<T> Add() 
        {
            Add(new T());
            _current = FindLast((x) => true);
            return this;
        }

        public DocumentsArrayFluent<T> First()
        {
            _current = Find((x) => true);
            return this;
        }

        public DocumentsArrayFluent<T> Next()
        {
            var i = IndexOf(_current);
            _current = this[i + 1];
            return this;
        }

        public bool IsLast()
        {
            var i = IndexOf(_current);
            return (i == Count - 1);
        }

        public DocumentsArrayFluent<T> DocumentType(DocumentType type ) 
        {
            _current.Type = type;
            return this;
        }

        public DocumentsArrayFluent<T> Size( Size size)
        {
            _current.Size = size;
            return this;
        }

        public DocumentsArrayFluent<T> FileFormat(FileFormat format )
        {
            _current.FileFormat = format;
            return this;
        }

        public DocumentsArrayFluent<T> ContentType(ContentType content)
        {
            _current.ContentType = content;
            return this;
        }

        public DocumentsArrayFluent<T> PrintDialogOption(PrintDialogOption option)
        {
            _current.PrintDialogOption = option;
            return this;
        }
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