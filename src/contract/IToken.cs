using System;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IToken
    {
        string AccessToken { get; set; }
        string ClientID { get; set; }
        long ExpiresIn { get; set; }
        DateTimeOffset IssuedAt { get; set; }
        string Org { get; set; }
        string TokenType { get; set; }
    }
}