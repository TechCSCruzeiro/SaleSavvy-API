using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Interface
{
    public interface IProductRepository
    {
        public Task<OutputProduct> SaveProduct(InputProduct input);
        public Task<OutputProduct> EditProduct();
        public Task<OutputProduct> DiscardProduct();
        public Task<Product[]> FindProduct(Guid id);

    }
}
