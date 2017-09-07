using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonShipment<T> : JsonWrapper<T>, IShipment where T : IShipment, new()
    {
        public JsonShipment() : base() { }

        public JsonShipment(T t) : base(t) { }

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
            get=>Wrapped.Parcel;
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
        [JsonProperty("customs")]
        public ICustoms Customs
        {
            get => Wrapped.Customs;
            set { Wrapped.Customs = value; }
        }
        [JsonProperty("shipmentId")]
        public string ShipmentId
        { 
            get => Wrapped.ShipmentId;
            set { Wrapped.ShipmentId = value; }
        }
        [JsonProperty("parcelTrackingNumber")]
        public string ParcelTrackingNumber
        {
            get => Wrapped.ParcelTrackingNumber;
            set { Wrapped.ParcelTrackingNumber = value; }
        }
    }
}