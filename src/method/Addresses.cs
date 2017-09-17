using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;


namespace PitneyBowes.Developer.ShippingApi.Method
{


    [JsonObject(MemberSerialization.OptIn)]
    public class AddressSuggestions
    {
        [JsonProperty("suggestionType")]
        string SuggestionType { get; set; } //TODO - figure out possible values and convert to enum
        [JsonProperty("address")]
        IEnumerable<IAddress> Addresses { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class VerifySuggestResponse
    {
        [JsonProperty("address")]
        IAddress Address { get; set; }
        [JsonProperty("suggestions")]
        AddressSuggestions Suggestions { get; set; }
    }

    public static class AddressessMethods
    {
        public async static Task<ShippingApiResponse<T>> VerifyAddress<T>(T request, ShippingApi.Session session = null) where T : IAddress, new()
        {
            var verifyRequest = new JsonAddress<T>(request);
            if (session == null) session = ShippingApi.DefaultSession;
            verifyRequest.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post<T, JsonAddress<T>>("/shippingservices/v1/addresses/verify", verifyRequest, session);
        }
        public async static Task<ShippingApiResponse<AddressSuggestions>> VerifySuggestAddress<T>(JsonAddress<T> request, ShippingApi.Session session = null) where T : IAddress, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            request.Suggest = true;
            return await WebMethod.Post<AddressSuggestions, JsonAddress<T>>("/shippingservices/v1/addresses/verify-suggest", request, session);
        }
    }
}
