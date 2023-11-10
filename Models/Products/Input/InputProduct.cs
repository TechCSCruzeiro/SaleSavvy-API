using SaleSavvy_API.Models.Sales.Input;
using System.Text.Json.Serialization;

namespace SaleSavvy_API.Models.Products.Input
{
    public class InputProduct
    {
        public InputProduct() { }
        public InputProduct(InputSaveProduct product, Guid id)
        {
            UserId = product.UserId;
            Product = new Product()
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

        public InputProduct(ProductSales product, Guid userId)
        {
            UserId = userId;
            Product = new Product()
            {
                Description = product.Description,
                Id = product.Id,
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
