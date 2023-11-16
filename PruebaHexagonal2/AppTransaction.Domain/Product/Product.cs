namespace AppTransaction.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public double ProductValue { get; set; }
    }
}
