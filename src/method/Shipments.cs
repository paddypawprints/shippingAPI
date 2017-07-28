using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using com.pb.shippingapi.model;
using System;
using System.Text;

namespace com.pb.shippingapi
{

    [JsonObject(MemberSerialization.OptIn)]
    public class CreateShipmentRequest : Shipment, IShippingApiRequest
    {
        [ShippingAPIHeader("ContentType")]
        public string ContentType { get; set; }
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
        [ShippingAPIHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingAPIHeader("X-PB-Shipper-Rate-Plan")]
        public string ShipperRatePlan {get;set;}
 
        [ShippingAPIQuery("includeDeliveryCommitment")]
        public bool includeDeliveryCommitment {get;set;}

        public CreateShipmentRequest(ShippingApi.Session session = null) : base()
        {
            if (session == null ) session = ShippingApi.DefaultSession;
            Authorization = new StringBuilder(session.AuthToken.AccessToken);
            ContentType="application/json";
        }

    }
    [JsonObject(MemberSerialization.OptIn)]
    public class CancelShipmentRequest : IShippingApiRequest
    {
        [ShippingAPIHeader("ContentType")]
        public string ContentType { get; set; }
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
        [ShippingAPIHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingAPIResource("shipments")]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        public string Carrier {get;set;}
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator {get;set;}

        public CancelShipmentRequest(ShippingApi.Session session = null) 
        {
            if (session == null ) session = ShippingApi.DefaultSession;
            Authorization = new StringBuilder(session.AuthToken.AccessToken);
            ContentType="application/json";
        }
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
    public class ReprintShipmentRequest : IShippingApiRequest
    {
        [ShippingAPIHeader("ContentType")]
        public string ContentType { get; set; }
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
        [ShippingAPIResource("shipments")]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        public string Carrier;
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator;
        public ReprintShipmentRequest(ShippingApi.Session session = null) 
        {
            if (session == null ) session = ShippingApi.DefaultSession;
            Authorization = new StringBuilder(session.AuthToken.AccessToken);
            ContentType="application/json";
        }

    }


    public static class ShipmentsMethods
    {
        public async static Task<ShippingAPIResponse<Shipment>> CreateShipment( CreateShipmentRequest request )
        {

            return await WebMethod.Post<Shipment, CreateShipmentRequest>( "/shippingservices/v1/shipments", request );
        }
        public async static Task<ShippingAPIResponse<CancelShipmentResponse>> CancelShipment( CancelShipmentRequest request)
        {
             return await WebMethod.Delete<CancelShipmentResponse, CancelShipmentRequest>( "/shippingservices/v1/shipments", request );
        }

        public async static Task<ShippingAPIResponse<Shipment>>  ReprintShipment(ReprintShipmentRequest request )
        {
             return await WebMethod.Get<Shipment, ReprintShipmentRequest>( "/shippingservices/v1/shipments", request );
        }

    }
}