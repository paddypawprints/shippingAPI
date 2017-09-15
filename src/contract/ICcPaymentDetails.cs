using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ICcPaymentDetails
    {
        CreditCardType CcType { get; set; } 
        string CcTokenNumber { get; set; }
        string CcExpirationDate { get; set; }
        string CccvvNumber { get; set; }
        IAddress CcAddress { get; set; }
    }
}
