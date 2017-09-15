using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IPaymentInfo
    {
        PaymentType PaymentType { get; set; }
        PaymentMethod PaymentMethod { get; set; }
        IPpPaymentDetails PpPaymentDetails { get; set; }
        ICcPaymentDetails CcPaymentDetails {get;set;}
    }
}
