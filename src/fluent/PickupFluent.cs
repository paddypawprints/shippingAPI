﻿using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Method;


namespace PitneyBowes.Developer.ShippingApi.Fluent
{
    /// <summary>
    /// An address. If part of a response, this object also specifies address validation status, unless minimum validation is enabled.
    /// <a href="https://shipping.pitneybowes.com/reference/resource-objects.html#object-address"/>
    /// </summary>
    public class PickupFluent<T> where T : IPickup, new()
    {
        private T _pickup;

        public static implicit operator T(PickupFluent<T> a)
        {
            return a._pickup;
        }

        public static PickupFluent<T> Create()
        {
            var a = new PickupFluent<T>()
            {
                _pickup = new T()
            };
            return a;
        }

        public PickupFluent()
        {
            _pickup = new T();
        }

        public PickupFluent(IAddress a)
        {
            _pickup = (T)a;
        }

        public PickupFluent<T> Schedule()
        {
            var response = PickupMethods.Schedule<T>(_pickup).GetAwaiter().GetResult();
            _pickup = response.APIResponse;
            return this;
        }

        public string Cancel()
        {
            var cancel = new PickupCancelRequest()
            {
                PickupId = _pickup.PickupId
            };

            var response = PickupMethods.CancelPickup(cancel).GetAwaiter().GetResult();
            var status = response.APIResponse.Status;
            return status;
        }


        public PickupFluent<T> PickupAddress( IAddress a)
        {
            _pickup.PickupAddress = a;
            return this;
        }
        public PickupFluent<T> Carrier( Carrier c)
        {
            _pickup.Carrier = c;
            return this;
        }
        public PickupFluent<T> PickupSummary(IEnumerable<IPickupCount> s)
        {
            foreach( var p in s)
            {
                _pickup.AddPickupCount(p);
            }
            return this;
        }
        public PickupFluent<T> PickupSummary(IPickupCount s)
        {
            _pickup.AddPickupCount(s);
            return this;
        }

        public PickupFluent<T> Reference(string s)
        {
            _pickup.Reference = s;
            return this;
        }
        public PickupFluent<T> PackageLocation(PackageLocation p)
        {
            _pickup.PackageLocation = p;
            return this;
        }
        public PickupFluent<T> SpecialInstructions(string s)
        {
            _pickup.SpecialInstructions = s;
            return this;
        }
        public PickupFluent<T> PickupDate( DateTime d)
        {
            _pickup.PickupDate = d;
            return this;
        }
        public PickupFluent<T> PickupConfirmationNumber( string p)
        {
            _pickup.PickupConfirmationNumber = p;
            return this;
        }
        public PickupFluent<T> PickupId(string p)
        {
            _pickup.PickupId = p;
            return this;
        }
    }
}
