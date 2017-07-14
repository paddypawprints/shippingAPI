using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using com.pb.shippingapi.model;

namespace com.pb.shippingapi
{

    public class CreateShipmentRequest : Shipment
    {
        [ShippingAPIHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingAPIHeader("X-PB-Shipper-Rate-Plan")]
        public string ShipperRatePlan {get;set;}
        [ShippingAPIQuery("includeDeliveryCommitment")]
        public bool includeDeliveryCommitment;

    }
   public class CancelShipmentRequest
    {
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

   public class ReprintShipmentRequest
    {
        [ShippingAPIResource("shipments")]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        public string Carrier;
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator;

    }


    public static class ShipmentsMethods
    {
        public async static Task<ShippingAPIResponse<Shipment>> CreateShipment(Token token, Shipment request )
        {

             return await WebMethod.Post<Shipment, Shipment>( request );
        }
        public async static Task<ShippingAPIResponse<CancelShipmentResponse>> CancelShipment(Token token, CancelShipmentRequest request)
        {
             return await WebMethod.Delete<CancelShipmentResponse, CancelShipmentRequest>( request );
        }

        public async static Task<ShippingAPIResponse<Shipment>>  ReprintShipment(Token token, ReprintShipmentRequest request )
        {
             return await WebMethod.Get<Shipment, ReprintShipmentRequest>( request );
        }

    }
}