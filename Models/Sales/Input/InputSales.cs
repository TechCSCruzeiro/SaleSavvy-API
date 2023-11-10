namespace SaleSavvy_API.Models.Sales.Input
{
    public class InputSales
    {
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public List<ProductSales> Product { get; set; }
        public int QuantityParcel { get; set; }
        public string Payment { get; set; }
        public decimal TotalPrice => CalculateTotalPrice();

        private decimal CalculateTotalPrice()
        {
            return Product.Sum(x => x.Price * x.Quantity);
        }
    }

    public class ProductSales
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<int> QuantityDisplay { get; set; }
    }
}
