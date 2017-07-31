using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using com.pb.shippingapi.model;

namespace com.pb.shippingapi.fluent
{
    public class DocumentsArrayFluent : List<Document>
    {
        public static DocumentsArrayFluent Create()
        {
            return new DocumentsArrayFluent();
        }
        protected Document _current = null;

        public DocumentsArrayFluent Add()
        {
            Add(new Document());
            _current = FindLast((x) => true);
            return this;
        }

        public DocumentsArrayFluent First()
        {
            _current = Find((x) => true);
            return this;
        }

        public DocumentsArrayFluent Next()
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

        public DocumentsArrayFluent DocumentType(DocumentType type ) 
        {
            _current.Type = type;
            return this;
        }

        public DocumentsArrayFluent Size( Size size)
        {
            _current.Size = size;
            return this;
        }

        public DocumentsArrayFluent FileFormat(FileFormat format )
        {
            _current.FileFormat = format;
            return this;
        }

        public DocumentsArrayFluent ContentType(ContentType content)
        {
            _current.ContentType = content;
            return this;
        }

        public DocumentsArrayFluent PrintDialogOption(PrintDialogOption option)
        {
            _current.printDialogOption = option;
            return this;
        }
    }
}