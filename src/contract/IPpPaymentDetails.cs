using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IPpPaymentDetails
    {
        string EncryptedTIN { get; set; }
        string EncryptedBPN { get; set; }
    }
}
