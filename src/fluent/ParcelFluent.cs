using Newtonsoft.Json;
using com.pb.shippingapi.model;

namespace com.pb.shippingapi.fluent
{
    public class ParcelFluent
    {
        private Parcel _parcel;

        public static ParcelFluent Create()
        {
            return new ParcelFluent();
        }
        public ParcelFluent()
        {
            _parcel = new Parcel();
            _parcel.CurrencyCode = "USD";
        }

        public static implicit operator Parcel( ParcelFluent p)
        {
            return p._parcel;
        }

        public ParcelFluent Height(decimal d, UnitOfDimension unit = UnitOfDimension.IN) 
        {
            if (_parcel.Dimension == null) _parcel.Dimension = new ParcelDimension();
            _parcel.Dimension.UnitOfMeasurement = unit;
            _parcel.Dimension.Height = d;
            return this;
        }
        public ParcelFluent Length(decimal d, UnitOfDimension unit = UnitOfDimension.IN) 
        {
            if (_parcel.Dimension == null) _parcel.Dimension = new ParcelDimension();
            _parcel.Dimension.UnitOfMeasurement = unit;
            _parcel.Dimension.Length = d;
            return this;
        }
        public ParcelFluent Width(decimal d, UnitOfDimension unit = UnitOfDimension.IN) 
        {
            if (_parcel.Dimension == null) _parcel.Dimension = new ParcelDimension();
            _parcel.Dimension.UnitOfMeasurement = unit;
            _parcel.Dimension.Width = d;
            return this;
        }

        public ParcelFluent Weight(decimal d, UnitOfWeight unit = UnitOfWeight.OZ) 
       {
            if (_parcel.Weight == null) _parcel.Weight = new model.ParcelWeight() { UnitOfMeasurement = unit};
            _parcel.Weight.Weight = d;
            return this;
        }

        public ParcelFluent ValueOfGoods( decimal d) 
        { 
            _parcel.ValueOfGoods = d;
            return this;
        }

        public ParcelFluent CurrencyCode( string s) 
        {
            _parcel.CurrencyCode = s;
            return this;
        }
        
    }
}