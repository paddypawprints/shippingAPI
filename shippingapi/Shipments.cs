using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace com.pb.shippingapi
{

    public class CreateShipmentRequest
    {
        [ShippingAPIHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingAPIHeader("X-PB-Shipper-Rate-Plan")]
        public string ShipperRatePlan {get;set;}
        [ShippingAPIQuery("includeDeliveryCommitment")]
        public bool includeDeliveryCommitment;

    }
    public class CreateShipmentResponse
    {
        public bool includeDeliveryCommitment;

    }
   public class CancelShipmentRequest
    {
        [ShippingAPIHeader("X-PB-TransactionId")]
        public string TransactionId {get;set;}
        [ShippingAPIResource("shipments")]
        public string ShipmentToCancel {get;set;}
        [JsonProperty("carrier")]
        public string Carrier;
        [JsonProperty("cancelInitiator")]
        public string CancelInitiator;

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


    public class Shipments
    {
        public async static Task<ShippingAPIResponse<CreateShipmentResponse>> CreateShipment(ShippingAPIAccessToken token, CreateShipmentRequest request )
        {
             return await WebMethod.Post<CreateShipmentResponse, CreateShipmentRequest>( request );
        }
        public async static Task<ShippingAPIResponse<CreateShipmentResponse>> CancelShipment(ShippingAPIAccessToken token, CancelShipmentRequest request)
        {
             return await WebMethod.Delete<CreateShipmentResponse, CancelShipmentRequest>( request );
        }

        public async static Task<ShippingAPIResponse<CreateShipmentResponse>>  ReprintShipment(ShippingAPIAccessToken token, ReprintShipmentRequest request )
        {
             return await WebMethod.Get<CreateShipmentResponse, ReprintShipmentRequest>( request );
        }

    }
}