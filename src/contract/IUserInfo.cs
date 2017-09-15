using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IUserInfo
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Company { get; set; }
        IAddress Address { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
    }
}
