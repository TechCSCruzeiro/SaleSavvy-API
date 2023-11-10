using Microsoft.IdentityModel.Tokens;
using SaleSavvy_API.Models.Products.Input;
using SaleSavvy_API.Models.Products.Output;

namespace SaleSavvy_API.Models.Validates
{
    public class ValidateProduct
    {
        public OutputProduct Validate(InputProduct product)
        {
            var output = new OutputProduct();
            var listError = new List<string>();

            if (product.UserId == null || product.UserId == Guid.Empty)
            {
                listError.Add("Id do usuario não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());

                return output;
            }

            if (string.IsNullOrEmpty(product.Product.Name))
            {
                listError.Add("Nome do Produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (product.Product.Quantity == null || product.Product.Quantity == 0)
            {
                listError.Add("Quantidade do produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(product.Product.Description))
            {
                listError.Add("Descrição do produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (product.Product.Price == null || product.Product.Price == 0m)
            {
                listError.Add("Preço do produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (output.Error != null)
            {
                return output;
            }

            output.ReturnCode = ReturnCode.exito;
            return output;
        }

        public OutputProduct ValidateInsert(InputSaveProduct product)
        {
            var output = new OutputProduct();
            var listError = new List<string>();

            if (product.UserId == null || product.UserId == Guid.Empty)
            {
                listError.Add("Id do usuario não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());

                return output;
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                listError.Add("Nome do Produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (product.Quantity == null || product.Quantity == 0)
            {
                listError.Add("Quantidade do produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                listError.Add("Descrição do produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (product.Price == null || product.Price == 0m)
            {
                listError.Add("Preço do produto não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (output.Error != null)
            {
                return output;
            }

            output.ReturnCode = ReturnCode.exito;
            return output;
        }

    }
}
