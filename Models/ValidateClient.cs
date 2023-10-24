using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Models
{
    public class ValidateClient
    {
        public OutputClient Validate(InputClient customer)
        {
            var output = new OutputClient();
            var listError = new List<string>();

            if (customer.UserID == null || customer.UserID == Guid.Empty)
            {
                listError.Add("Id do usuario não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());

                return output;
            }

            if (string.IsNullOrEmpty(customer.Phone))
            {
                listError.Add("Campo de Contato não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Email))
            {
                listError.Add("Campo de Email não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                listError.Add("Campo Nome não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (customer.Address != null)
            {
                var validateAdress = new ValidateAddress().Validate(customer);

                if(validateAdress.ReturnCode == ReturnCode.failed)
                {
                    output.AddError(validateAdress.Error.MenssageError.ToArray());
                }
            }
            else
            {
                listError.Add("Endereço não pode ser vazio");
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
