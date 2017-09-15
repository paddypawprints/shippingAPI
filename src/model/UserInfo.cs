using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class UserInfo : IUserInfo
    {

        public string FirstName{get; set;}
        public string LastName{get; set;}
        public string Company{get; set;}
        public IAddress Address{get; set;}
        public string Phone{get; set;}
        public string Email{get; set;}
    }
}
