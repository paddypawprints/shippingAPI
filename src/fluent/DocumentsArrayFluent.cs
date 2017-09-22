
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
        public DocumentsArrayFluent<T> ShippingLabel( ContentType contentType = Developer.ShippingApi.ContentType.URL, Size size = Developer.ShippingApi.Size.DOC_8X11, FileFormat fileFormat = Developer.ShippingApi.FileFormat.PDF)
        {
            return Add()
                .DocumentType(Developer.ShippingApi.DocumentType.SHIPPING_LABEL)
                .ContentType(contentType)
                .Size(size)
                .FileFormat(fileFormat)
                .PrintDialogOption(Developer.ShippingApi.PrintDialogOption.NO_PRINT_DIALOG);
        }
    }
}