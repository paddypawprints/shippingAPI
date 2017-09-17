using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CcPaymentDetails : ICcPaymentDetails
    {
        virtual public CreditCardType CcType{get; set;}
        virtual public string CcTokenNumber{get; set;}
        virtual public string CcExpirationDate{get; set;}
        virtual public string CccvvNumber{get; set;}
        virtual public IAddress CcAddress{get; set;}
    }
}
