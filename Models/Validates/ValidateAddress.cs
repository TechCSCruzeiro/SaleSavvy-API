using SaleSavvy_API.Models.Client.Input;
using SaleSavvy_API.Models.Client.Output;

namespace SaleSavvy_API.Models.Validates
{
    public class ValidateAddress
    {
        public OutputClient Validate(InputClient customer)
        {
            var output = new OutputClient();
            var listError = new List<string>();

            if (string.IsNullOrEmpty(customer.Address.Code))
            {
                listError.Add("Campo Code de Endereço não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Address.State))
            {
                listError.Add("Campo Estado de Endereço não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Address.Street))
            {
                listError.Add("Campo Rua de Endereço não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Address.City))
            {
                listError.Add("Campo Cidade de Endereço não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Address.District))
            {
                listError.Add("Campo Bairro de Endereço não pode ser vazio ou nulo");
                output.AddError(listError.ToArray());
            }

            if (string.IsNullOrEmpty(customer.Address.Number))
            {
                listError.Add("Campo Bairro de Endereço não pode ser vazio ou nulo");
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
