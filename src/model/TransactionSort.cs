namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TransactionSort : ITransactionSort
    {

        public string Ascending { get; set; }
        public string Direction { get; set; }
        public string IgnoreCase { get; set; }
        public string NullHandling { get; set; }
        public string Property { get; set; }
    }
}
