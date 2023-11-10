using Newtonsoft.Json;
using SaleSavvy_API.Models.Sales.Input;
using System.Globalization;

namespace SaleSavvy_API.Models.Products
{
    public class Product
    {
        public Product() { }

        public Product(ProductSales product, ProductDto dto, decimal? currentQuantity)
        {
            Id = product.Id;
            Name = dto.Name;
            Description = dto.Description;
            Price = dto.Price;
            Quantity = product.Quantity;
            CreationDate = DateTime.Parse(dto.CreationDate);
            IsActive = dto.IsActive;
            CurrentQuantity = currentQuantity - product.Quantity;
        }

        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
        public decimal? CurrentQuantity { get; set; }
    }
}
