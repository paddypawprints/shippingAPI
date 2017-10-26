using System;
using System.Collections.Generic;
using System.Net;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingAPIException : Exception
    {
        public ShippingApiResponse ErrorResponse { get; set; }

        public ShippingAPIException()
        {
        }

        public ShippingAPIException(ShippingApiResponse response) : this()
        {
            ErrorResponse = response;
        }

        public ShippingAPIException(ShippingApiResponse response, string message): this(message)
        {
            ErrorResponse = response;
        }
        public ShippingAPIException(string message): base(message)
        {
        }

        public ShippingAPIException(string message, Exception inner): base(message, inner)
        {
        }
        public ShippingAPIException(ShippingApiResponse response, string message, Exception inner): this(message, inner)
        {
            ErrorResponse = response;
        }

    }
}
