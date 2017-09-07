using System;
using PitneyBowes.Developer.ShippingApi.Model;

namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    public class ParcelFluent<T> where T : IParcel, new()
    {
        private IParcel _parcel;

        public static ParcelFluent<T> Create()
        {
            var p = new ParcelFluent<T>() { _parcel = new T() };
            p._parcel.CurrencyCode = "USD";
            return p;
        }

 
        private ParcelFluent()
        {
         
        }

        public static implicit operator T( ParcelFluent<T> p)
        {
            return (T)p._parcel;
        }

        public ParcelFluent<T> Dimension(decimal l, decimal h, decimal w, UnitOfDimension u = UnitOfDimension.IN) 
        {
            _parcel.Dimension = new ParcelDimension() { Length = l, Height = h, Width = w, UnitOfMeasurement = u };
            return this;
        }


        public ParcelFluent<T> Weight(decimal d, UnitOfWeight unit = UnitOfWeight.OZ) 
       {
            _parcel.Weight = new ParcelWeight() { Weight = d, UnitOfMeasurement = unit };
            return this;
        }

        public ParcelFluent<T> ValueOfGoods( decimal d) 
        { 
            _parcel.ValueOfGoods = d;
            return this;
        }

        public ParcelFluent<T> CurrencyCode( string s) 
        {
            _parcel.CurrencyCode = s;
            return this;
        }
        
    }
}