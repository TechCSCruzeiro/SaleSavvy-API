using Microsoft.IdentityModel.Tokens;

namespace SaleSavvy_API.Models.Products
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

    }
}
