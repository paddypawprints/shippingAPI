using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonUserInfo<T> : JsonWrapper<T>, IUserInfo where T : IUserInfo, new()
    {
        public JsonUserInfo() : base() { }
        public JsonUserInfo(T t) : base(t) { }

        [JsonProperty("firstName")]
        public string FirstName
        {
            get => Wrapped.FirstName;
            set { Wrapped.FirstName = value; }
        }
        [JsonProperty("lastName")]
        public string LastName
        {
            get => Wrapped.LastName;
            set { Wrapped.LastName = value; }
        }
        [JsonProperty("company")]
        public string Company
        {
            get => Wrapped.Company;
            set { Wrapped.Company = value; }
        }
        [JsonProperty("address")]
        public IAddress Address
        {
            get => Wrapped.Address;
            set { Wrapped.Address = value; }
        }
        [JsonProperty("phone")]
        public string Phone
        {
            get => Wrapped.Phone;
            set { Wrapped.Phone = value; }
        }
        [JsonProperty("email")]
        public string Email
        {
            get => Wrapped.Email;
            set { Wrapped.Email = value; }
        }
    }
}
