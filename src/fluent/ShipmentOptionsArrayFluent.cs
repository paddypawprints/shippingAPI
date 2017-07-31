using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using com.pb.shippingapi.model;


namespace com.pb.shippingapi.fluent
{
    public class ShipmentOptionsArrayFluent : List<ShipmentOptions>
    {
        public static ShipmentOptionsArrayFluent Create()
        {
            return new ShipmentOptionsArrayFluent();
        }
        protected ShipmentOptions _current = null;

        public ShipmentOptionsArrayFluent Add()
        {
            Add(new ShipmentOptions());
            _current = FindLast((x) => true);
            return this;
        }

        public ShipmentOptionsArrayFluent First()
        {
            _current = Find((x) => true);
            return this;
        }

        public ShipmentOptionsArrayFluent Next()
        {
            var i = IndexOf(_current);
            _current = this[i + 1];
            return this;
        }

        public bool IsLast()
        {
            var i = IndexOf(_current);
            return (i == Count - 1);
        }
        public ShipmentOptionsArrayFluent Option(ShipmentOption option, string value ) 
        {
            _current.ShipmentOption = option;
            _current.Value = value;
            return this;
        }

    }
}