using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Services
{
    public class ProductsService : IProductsService
    {
        IProductRepository _productRepository;

        public ProductsService(IProductRepository productRepository) 
        {
             this._productRepository = productRepository;
        }

        public async Task<OutputProduct> CreateProduct(InputProduct input)
        {
            var validate = new ValidateProduct().Validate(input);
            
            if(validate.ReturnCode == Models.ReturnCode.failed)
            {
                return validate;
            }

            return await _productRepository.SaveProduct(input);
        }

        public Task<OutputProduct> ModifyProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> RemoveProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> SearchProduct()
        {
            throw new NotImplementedException();
        }
    }
}
