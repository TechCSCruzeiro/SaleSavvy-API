using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Login.Output;

namespace SaleSavvy_API.Models.Login
{
    public class ValidateLogin
    {

        public OutputGetLogin ValidateInsertion(InputLogin input)
        {
            var minimumPasswordLength = 8;
            var maximumPasswordLength = 20;
            var listError = new List<string>();
            var output = new OutputGetLogin();

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

            return output;
        }
    }
}
