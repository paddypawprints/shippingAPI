namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class TransactionSort : ITransactionSort
    {
        virtual public string Ascending { get; set; }
        virtual public string Direction { get; set; }
        virtual public string IgnoreCase { get; set; }
        virtual public string NullHandling { get; set; }
        virtual public string Property { get; set; }
    }
}
