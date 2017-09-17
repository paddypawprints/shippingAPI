using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class PaymentInfo : IPaymentInfo
    {
        virtual public PaymentType PaymentType{get; set;}
        virtual public PaymentMethod PaymentMethod{get; set;}
        virtual public IPpPaymentDetails PpPaymentDetails{get; set;}
        virtual public ICcPaymentDetails CcPaymentDetails{get; set;}
    }
}
