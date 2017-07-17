using System;
using Xunit;
using com.pb.shippingapi;

namespace test
{
    public class EnvironmentTest
    {
        [Fact]
        public void Test1()
        {
            ShippingApi.Init();
            ShippingApi.DefaultSession="sandbox";
        }
    }
}
