using PitneyBowes.Developer.ShippingApi;
using PitneyBowes.Developer.ShippingApi.Model;
using PitneyBowes.Developer.ShippingApi.Fluent;

namespace PitneyBowes.Developer.ShippingApi
{
    public static class USPSExtensions
    {
        public static RatesArrayFluent<T> USPSPriority<T>(this RatesArrayFluent<T> f) where T: class, IRates, new()
        {
            return f.Add().Carrier(Carrier.USPS)
                .ParcelType(ParcelType.PKG)
                .Service(Services.PM)
                .SpecialService<SpecialServices>(SpecialServiceCodes.DelCon, 0M, new Parameter("INPUT_VALUE", "0"));
        }
    }
}
