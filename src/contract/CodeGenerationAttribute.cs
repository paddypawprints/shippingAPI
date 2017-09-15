using System;
using System.Collections.Generic;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public class CodeGenerationAttribute : Attribute
    {
        public bool GenerateModel { get; set; }
        public bool GenerateJsonWrapper { get; set; }
    }

}
