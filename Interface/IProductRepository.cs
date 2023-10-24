﻿using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Interface
{
    public interface IProductRepository
    {
        public Task<OutputProduct> SaveProduct(InputSaveProduct input, Guid id);
        public Task<OutputProduct> EditProduct(Product product);
        public Task<OutputProduct> DiscardProduct(Guid productId);
        public Task<ProductDto[]> FindProduct(Guid id);
        public Task<ProductDto> FindProductById(Guid id);
    }
}
