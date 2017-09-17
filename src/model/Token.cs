 using System;


 
 namespace PitneyBowes.Developer.ShippingApi.Model
 {
    public class Token : IToken
    {
        virtual public string AccessToken {get;set;}
        virtual public string TokenType {get;set;}
        virtual public DateTimeOffset IssuedAt { get;set;}
        virtual public long ExpiresIn {get;set;}
        virtual public string ClientID { get;set;}
        virtual public string Org { get;set;}
    }
 }