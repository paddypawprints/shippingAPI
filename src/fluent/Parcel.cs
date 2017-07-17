using Newtonsoft.Json;

namespace com.pb.shippingapi.fluent
{
    public class Parcel
    {
        private model.Parcel _parcel;
        private model.UnitOfDimension _unitOfDimension;
        private model.UnitOfWeight _unitOfWeight;

        public static Parcel Create()
        {
            return new Parcel();
        }
        public Parcel()
        {
            _parcel = new model.Parcel();
            _unitOfDimension = model.UnitOfDimension.IN;
            _parcel.CurrencyCode = "USD";
        }

        public static implicit operator model.Parcel( Parcel p)
        {
            return p._parcel;
        }

        public Parcel inches()
        {
            _unitOfDimension = model.UnitOfDimension.IN;
            return this;
        }

        public Parcel cm()
        {
            _unitOfDimension = model.UnitOfDimension.CM;
            return this;
        }
        public Parcel Height(decimal d) 
        {
            if (_parcel.dimension == null) _parcel.dimension = new model.ParcelDimension(){ UnitOfMeasurement = _unitOfDimension};;
            _parcel.dimension.Height = d;
            return this;
        }
        public Parcel Length(decimal d) 
        {
            if (_parcel.dimension == null) _parcel.dimension = new model.ParcelDimension(){ UnitOfMeasurement = _unitOfDimension};;
            _parcel.dimension.Length = d;
            return this;
        }
        public Parcel Width(decimal d) 
        {
            if (_parcel.dimension == null) _parcel.dimension = new model.ParcelDimension() { UnitOfMeasurement = _unitOfDimension};
             _parcel.dimension.Width = d;
            return this;
        }

        public Parcel oz()
        {
            _unitOfWeight = model.UnitOfWeight.OZ;
            return this;
        }
        public Parcel gm()
        {
            _unitOfWeight = model.UnitOfWeight.GM;
            return this;
        }
        public Parcel Weight(decimal d) 
       {
            if (_parcel.Weight == null) _parcel.Weight = new model.ParcelWeight() { UnitOfMeasurement = _unitOfWeight};
             _parcel.Weight.Weight = d;
            return this;
        }

        public Parcel ValueOfGoods( decimal d) 
        { 
            _parcel.ValueOfGoods = d;
            return this;
        }

        public Parcel CurrencyCode( string s) 
        {
            _parcel.CurrencyCode = s;
            return this;
        }
        
    }
}