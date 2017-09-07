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