using System;

namespace com.pb.shippingapi
{
    public class ShippingAPIAttribute : Attribute
    {
        public string Name { get;set; }
        public ShippingAPIAttribute(string name)
        {
            Name = name;
        }

    }

    public class ShippingAPIHeaderAttribute : ShippingAPIAttribute
    {
        public bool OmitIfEmpty { get; set; }

        public ShippingAPIHeaderAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }
    public class ShippingAPIQueryAttribute : ShippingAPIAttribute
    {
        public bool OmitIfEmpty { get; set; }
        public ShippingAPIQueryAttribute(string name, bool omitIfEmpty = true) : base(name)
        {
            OmitIfEmpty = omitIfEmpty;
        }

    }

    public class ShippingAPIResourceAttribute : ShippingAPIAttribute
    {
        public bool AddId { get; set; }
        public ShippingAPIResourceAttribute(string name, bool addId = true) : base(name)
        {
            AddId = addId;
        }

    }
}