using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register.Output;

namespace SaleSavvy_API.Models.Validates
{
    public class ValidateRegister
    {
        public OutputRegister ValidateInsertRegister(InputRegister input)
        {

            var minimumPasswordLength = 8;
            var maximumPasswordLength = 20;
            var listError = new List<string>();
            var output = new OutputRegister();

            //Validar se senha entra na regra de min e max
            if (input.Password.Length < minimumPasswordLength || input.Password.Length > maximumPasswordLength)
            {
                listError.Add("Senha nao se encaixa no padrão do sistema");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            //Validar se email é vazio ou null
            if (string.IsNullOrEmpty(input.Email))
            {
                listError.Add("Campo email não pode ser vazio");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            if (string.IsNullOrEmpty(input.Name))
            {
                listError.Add("Campo Nome não pode ser vazio");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            return output;
        }
    }
}
