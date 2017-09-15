using System.Threading.Tasks;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;
using System.Text;
using System.Collections.Generic;
using System;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateShipmentRequest<T> : JsonWrapper<T>, IShippingApiRequest where T:IShipment, new()
    { 

        [ShippingAPIHeader("x-pb-transactionId", true)]
        public string TransactionId { get; set; }

        [ShippingAPIHeader("ContentType", true)]
        public string ContentType { get => "application/json"; }

        [ShippingAPIHeader("Bearer", true)]
        public StringBuilder Authorization { get; set; }

        [ShippingAPIHeader("minimaladdressvalidation", true)]
        public string MinimalAddressValidation { get; set; }

        [ShippingAPIHeader("x-pb-shipper-rate-plan", true)]
        public string ShipperRatePlan { get; set; }

        [JsonProperty("fromAddress")]
        public IAddress FromAddress
        {
            get => Wrapped.FromAddress;
            set { Wrapped.FromAddress = value; }
        }
        [JsonProperty("toAddress")]
        public IAddress ToAddress
        {
            get => Wrapped.ToAddress;
            set { Wrapped.ToAddress = value; }
        }
        [JsonProperty("altReturnAddress")]
        public IAddress AltReturnAddress
        {
            get => Wrapped.AltReturnAddress;
            set { Wrapped.AltReturnAddress = value; }
        }
        [JsonProperty("parcel")]
        public IParcel Parcel
        {
            get => Wrapped.Parcel;
            set { Wrapped.Parcel = value; }
        }
        [JsonProperty("rates")]
        public IEnumerable<IRates> Rates
        {
            get => Wrapped.Rates;
            set { Wrapped.Rates = value; }
        }
        public IRates AddRates(IRates r)
        {
            return Wrapped.AddRates(r);
        }
        [JsonProperty("documents")]
        public IEnumerable<IDocument> Documents
        {
            get => Wrapped.Documents;
            set { Wrapped.Documents = value; }
        }
        public IDocument AddDocument(IDocument d)
        {
            return Wrapped.AddDocument(d);
        }

        [JsonProperty("shipmentOptions")]
        public IEnumerable<IShipmentOptions> ShipmentOptions
        {
            get => Wrapped.ShipmentOptions;
            set { Wrapped.ShipmentOptions = value; }
        }
        public IShipmentOptions AddShipmentOptions(IShipmentOptions s)
        {
            return Wrapped.AddShipmentOptions(s);
        }

        public string GetUri(string baseUrl)
        {
            return baseUrl;
        }

        public IEnumerable<Tuple<ShippingAPIHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ShippingApi.Session session)
        {
            ShippingApiRequest.SerializeBody(this, writer, session);
        }

        [JsonProperty("customs")]
        public ICustoms Customs
        {
            get => Wrapped.Customs;
            set { Wrapped.Customs = value; }
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CancelShipmentRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }
        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingAPIHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingAPIResource("shipments")]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        public string Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator {get;set;}

    }
   public class CancelShipmentResponse 
    {
        [JsonProperty("carrier")]
        public string Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator {get;set;}
        [JsonProperty("totalCarrierCharge")]
        public decimal TotalCarrierCharge {get;set;}
        [JsonProperty("parcelTrackingNumber")]
        public string ParcelTrackingNumber {get;set;}
        [JsonProperty("status")]
        public RefundStatus RefundStatus {get;set;}

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ReprintShipmentRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }
        [ShippingAPIHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingAPIResource("shipments")]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        public string Carrier;
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator;
    }


    public static class ShipmentsMethods
    {

        public async static Task<ShippingAPIResponse<T>> CreateShipment<T>(CreateShipmentRequest<T> request, ShippingApi.Session session = null) where T:IShipment, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Post< T, CreateShipmentRequest<T>>( "/shippingservices/v1/shipments", request, session );
        }
        public async static Task<ShippingAPIResponse<CancelShipmentResponse>> CancelShipment( CancelShipmentRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Delete<CancelShipmentResponse, CancelShipmentRequest>( "/shippingservices/v1/shipments", request, session );
        }

        public async static Task<ShippingAPIResponse<T>>  ReprintShipment<T>(ReprintShipmentRequest request, ShippingApi.Session session = null) where T : IShipment, new()
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return  await WebMethod.Get<T, ReprintShipmentRequest>( "/shippingservices/v1/shipments", request, session );
        }

    }
}