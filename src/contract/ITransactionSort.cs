using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    [CodeGeneration(GenerateJsonWrapper = true, GenerateModel = false)]
    public interface ITransactionSort
    {
        string Ascending { get; set; }
        string Direction { get; set; }
        string IgnoreCase { get; set; }
        string NullHandling { get; set; }
        string Property { get; set; }
    }
}
