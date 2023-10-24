namespace SaleSavvy_API.Models.Products
{
    public class ProductDto
    {
        private readonly string DateFormat = "ddMMyyyy";

        public ProductDto() { }
        public ProductDto(Product product)
        {
            this.Description = product.Description;
            this.CreationDate = new DateTime(product.CreationDate.Value.Year, product.CreationDate.Value.Month, product.CreationDate.Value.Day, product.CreationDate.Value.Hour, product.CreationDate.Value.Minute, product.CreationDate.Value.Second).ToString();
            this.Price = product.Price;
            this.Quantity = product.Quantity;
            this.Name = product.Name;
            this.Id = product.Id;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? CreationDate { get; set; }
    }
}
