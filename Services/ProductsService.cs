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

        public async Task<OutputProduct> CreateProduct(InputSaveProduct input)
        {
            var id = Guid.NewGuid();

            var validate = new ValidateProduct().ValidateInsert(input);
           
            if(validate.ReturnCode == Models.ReturnCode.failed)
            {
                return validate;
            }

            var insertProduct = await _productRepository.SaveProduct(input, id ); 

            if(insertProduct.ReturnCode == Models.ReturnCode.exito) 
            {
                var output = await _movementRecordsRepository.SaveRecord(Models.StatusMovementRecords.Entrada, new InputProduct(input, id));
                return output;
            }

            return insertProduct;
        }

        public async Task<OutputProduct> UpdateProduct(InputProduct product)
        {
            var output = new OutputProduct();

            var validateProduct = await _productRepository.FindProductById(product.Product.Id);

            if( validateProduct == null) 
            {
                var listError = new List<string>();
                listError.Add("Produto não encontrado");

                output.AddError(listError.ToArray());
            }
            else
            {
                if(product.Product.IsActive == null)
                {
                    product.Product.IsActive = true;
                }

                output = await _productRepository.EditProduct(product.Product);
                
                if(output.ReturnCode == ReturnCode.exito)
                {
                    var status = StatusMovementRecords.Alteracao;

                    if(product.Product.Price != validateProduct.Price)
                    {
                        status = StatusMovementRecords.AlteracaoValor;
                        product.OldPrice = validateProduct.Price;
                    }

                    var saveRecord = await _movementRecordsRepository.SaveRecord(status , product);

                    if(saveRecord.ReturnCode != ReturnCode.exito) 
                    {
                        output.AddError(saveRecord.Error.MenssageError);
                    }
                }
                else
                {
                    output.AddError(output.Error.MenssageError);
                }
            }

            return output;
        }

        public async Task<OutputProduct> RemoveProduct(Guid productId)
        {
           var validateProduct = await _productRepository.FindProductById(productId);

            if(validateProduct == null)
            {
                var product = new OutputProduct();

                var listError = new List<string>();
                listError.Add("Produto não encontrado");
                product.AddError(listError.ToArray());

                return product;
            }

            return await _productRepository.DiscardProduct(productId);
        }

        public async Task<ProductDto[]> SearchProduct(Guid userId)
        {
            return await _productRepository.FindProduct(userId);
        }

        public async Task<ProductDto> SearchProductById(Guid productId)
        {
            return await _productRepository.FindProductById(productId);
        }
    }
}
