
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Model;


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class ShipmentOptionsArrayFluent<T> : List<T> where T : class, IShipmentOptions, new()
    {
        public static ShipmentOptionsArrayFluent<T> Create()
        {
            return new ShipmentOptionsArrayFluent<T>();
        }
        protected T _current = null;

        public ShipmentOptionsArrayFluent<T> Add() 
        {
            Add(new T());
            _current = FindLast((x) => true);
            return this;
        }

        public ShipmentOptionsArrayFluent<T> First()
        {
            _current = Find((x) => true);
            return this;
        }

        public ShipmentOptionsArrayFluent<T> Next()
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
        public ShipmentOptionsArrayFluent<T> Option(ShipmentOption option, string value ) 
        {
            _current.ShipmentOption = option;
            _current.Value = value;
            return this;
        }

    }
}