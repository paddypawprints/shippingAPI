using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class UserInfo : IUserInfo
    {
        virtual public string FirstName{get; set;}
        virtual public string LastName{get; set;}
        virtual public string Company{get; set;}
        virtual public IAddress Address{get; set;}
        virtual public string Phone{get; set;}
        virtual public string Email{get; set;}
    }
}
