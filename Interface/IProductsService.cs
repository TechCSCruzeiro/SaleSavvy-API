using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Interface
{
    public interface IProductsService
    {
        public Task<OutputProduct> CreateProduct(InputProduct input);
        public Task<Product[]> SearchProduct(Guid userId);
        public Task<OutputProduct> ModifyProduct();
        public Task<OutputProduct> RemoveProduct();
    }
}
