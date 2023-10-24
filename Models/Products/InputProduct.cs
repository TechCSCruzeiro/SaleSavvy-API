using System.Text.Json.Serialization;

namespace SaleSavvy_API.Models.Products
{
    public class InputProduct
    {
        public InputProduct() { }
        public InputProduct(InputSaveProduct product, Guid id) 
        {
            this.UserId = product.UserId;
            this.Product = new Product()
            {
                CreationDate = product.CreationDate,
                Description = product.Description,
                Id = id,
                IsActive = true,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        public Guid UserId { get; set; }
        public Product Product { get; set; }

        [JsonIgnore]
        public decimal? OldPrice { get; set; }

    }
}
