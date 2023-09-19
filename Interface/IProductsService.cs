using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Interface
{
    public interface IProductsService
    {
        public Task<OutputProduct> CreateProduct(InputProduct input);
        public Task<OutputProduct> SearchProduct();
        public Task<OutputProduct> ModifyProduct();
        public Task<OutputProduct> RemoveProduct();
    }
}
