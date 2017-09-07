
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class Document : IDocument
    {
        virtual public DocumentType Type { get; set;}
        virtual public Size Size { get; set;}
        virtual public FileFormat FileFormat { get;set;}
        virtual public ContentType ContentType {get;set;}
        virtual public PrintDialogOption PrintDialogOption {get;set;}
        virtual public string Contents {get;set;}
        virtual public IEnumerable<string> Pages { get; set; }
        virtual public void AddPage(string s)
        {
            ModelHelper.AddToEnumerable<string, string>(s, () => Pages, (x) => Pages = x);
        }
    }

}