using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Models.Products.Input;
using SaleSavvy_API.Models.Products.Output;

namespace SaleSavvy_API.Interface
{
    public interface IProductsService
    {
        public Task<OutputProduct> CreateProduct(InputSaveProduct input);
        public Task<ProductDto[]> SearchProduct(Guid userId);
        public Task<ProductDto> SearchProductById(Guid productId);
        public Task<OutputProduct> UpdateProduct(InputProduct product);
        public Task<OutputProduct> RemoveProduct(Guid productId);
    }
}
