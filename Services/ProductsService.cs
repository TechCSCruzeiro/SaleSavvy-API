using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Services
{
    public class ProductsService : IProductsService
    {
        IProductRepository _productRepository;
        IMovementRecordsRepository _movementRecordsRepository;

        public ProductsService(IProductRepository productRepository, IMovementRecordsRepository movementRecordsRepository ) 
        {
             this._productRepository = productRepository;
             this._movementRecordsRepository = movementRecordsRepository;
        }

        public async Task<OutputProduct> CreateProduct(InputProduct input)
        {
            input.Product.Id = Guid.NewGuid();
            var validate = new ValidateProduct().Validate(input);
            
            if(validate.ReturnCode == Models.ReturnCode.failed)
            {
                return validate;
            }

            var insertProduct = await _productRepository.SaveProduct(input); 

            if(insertProduct.ReturnCode == Models.ReturnCode.exito) 
            {
                var output = await _movementRecordsRepository.SaveRecord(Models.StatusMovementRecords.Entrada, input);
                return output;
            }

            return insertProduct;
        }

        public Task<OutputProduct> ModifyProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> RemoveProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<Product[]> SearchProduct(Guid userId)
        {
            return await _productRepository.FindProduct(userId);
        }
    }
}
