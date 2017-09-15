using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IAutoRefill
    {
        string MerchantID { get; set; }
        decimal Threshold { get; set; }
        decimal AddAmount { get; set; }
        bool Enabled { get; set; }
    }
}
