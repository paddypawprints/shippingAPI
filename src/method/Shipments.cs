using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PitneyBowes.Developer.ShippingApi.Json;
using System.Text;
using System.Collections.Generic;
using System;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi.Method
{
    /// <summary>
    /// Request object when getting a shipping label.
    /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-shipment">address documentation</a>
    /// </summary>
    /// 
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateShipmentRequest<T> : JsonWrapper<T>, IShippingApiRequest where T:IShipment, new()
    {

        public string RecordingSuffix => "";
        public string RecordingFullPath(string resource, ISession session)
        {
            return ShippingApiRequest.RecordingFullPath(this, resource, session);
        }

        [ShippingApiHeader("x-pb-transactionId", true)]
        public string TransactionId { get; set; }

        [ShippingApiHeader("ContentType", true)]
        public string ContentType { get => "application/json"; }

        [ShippingApiHeader("Bearer", true)]
        public StringBuilder Authorization { get; set; }

        [ShippingApiHeader("minimaladdressvalidation", true)]
        public string MinimalAddressValidation { get; set; }

        [ShippingApiHeader("x-pb-shipper-rate-plan", true)]
        public string ShipperRatePlan { get; set; }
        
        /// <summary>
        /// Shippers address.
        /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-shipment"/>
        /// </summary>
        [JsonProperty("fromAddress")]
        public IAddress FromAddress
        {
            get => Wrapped.FromAddress;
            set { Wrapped.FromAddress = value; }
        }
        /// <summary>
        /// Destination of the shipment.
        /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
        /// </summary>
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

        public IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return ShippingApiRequest.GetHeaders(this);
        }

        public void SerializeBody(StreamWriter writer, ISession session)
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
        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingApiHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingApiResource("shipments", AddId = true)]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Carrier Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CancelInitiator CancelInitiator {get;set;}
    }

   public class CancelShipmentResponse 
    {
        [JsonProperty("carrier")]
        public Carrier Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        public CancelInitiator CancelInitiator {get;set;}
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
        [ShippingApiHeader("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingApiResource("shipments", AddId = true)]
        public string Shipment {get;set;}
    }

    public static class ShipmentsMethods
    {
        public async static Task<ShippingApiResponse<T>> CreateShipment<T>(CreateShipmentRequest<T> request, ISession session = null) where T:IShipment, new()
        {
            return await WebMethod.Post< T, CreateShipmentRequest<T>>( "/shippingservices/v1/shipments", request, session );
        }
        public async static Task<ShippingApiResponse<CancelShipmentResponse>> CancelShipment( CancelShipmentRequest request, ISession session = null)
        {
            return await WebMethod.DeleteWithBody<CancelShipmentResponse, CancelShipmentRequest>( "/shippingservices/v1", request, session );
        }
        public async static Task<ShippingApiResponse<T>>  ReprintShipment<T>(ReprintShipmentRequest request, ISession session = null) where T : IShipment, new()
        {
            return  await WebMethod.Get<T, ReprintShipmentRequest>( "/shippingservices/v1", request, session );
        }
    }
}