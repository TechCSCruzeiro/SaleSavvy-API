using OfficeOpenXml.Style;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Models.Sales.Input;
using SaleSavvy_API.Models.Sales.Output;

namespace SaleSavvy_API.Models.Validates
{
    public class ValidateSales
    {
        public OutputSales Validate(InputSales input)
        {
            var output = new OutputSales();
            var listError = new List<string>();

            output.ReturnCode = ReturnCode.exito;

            if (input.Product == null || input.Product.Count == 0)
            {
                listError.Add("Produto não pode ser vazio");
                output.AddError(listError.ToArray());
            }
            else
            {
                foreach (var product in input.Product)
                {
                    var validateProduct = ValidateProd(product);

                    if (validateProduct.ReturnCode != ReturnCode.exito)
                    {
                        output.AddError(validateProduct.Error.MenssageError);
                    }
                }
            }

            if (string.IsNullOrEmpty(input.Payment))
            {
                listError.Add("O campo de Pagamento não pode ser nulo ou vazio.\"");
                output.AddError(listError.ToArray());
            }

            return output;

        }

        static OutputSales ValidateProd(ProductSales product)
        {
            var output = new OutputSales();
            var listError = new List<string>();

            output.ReturnCode = ReturnCode.exito;

            if (string.IsNullOrEmpty(product.Name))
            {
                listError.Add("O campo Nome do produto não pode ser nulo ou vazio.");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                listError.Add("O campo Descrição do produto não pode ser nulo ou vazio");
                output.AddError(listError.ToArray());
            }

            if (product.Price <= 0)
            {
                listError.Add("O campo Preço do produto deve ser maior que zero.");
                output.AddError(listError.ToArray());
            }

            if (product.Quantity <= 0)
            {
                listError.Add("O campo Quantidade do produto deve ser maior que zero.");
                output.AddError(listError.ToArray());
            }

            if (product.QuantityDisplay == null || product.QuantityDisplay.Count == 0)
            {
                listError.Add("A lista QuantityDisplay do produto não pode ser nula ou vazia.");
                output.AddError(listError.ToArray());
            }

            return output;

        }
    }
}
