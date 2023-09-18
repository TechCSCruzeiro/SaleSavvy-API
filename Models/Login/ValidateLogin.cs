using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Login.Output;

namespace SaleSavvy_API.Models.Login
{
    public class ValidateLogin
    {

        public OutputGetLogin ValidateInsertion(Guid Id, string email, string password)
        {
            var minimumPasswordLength = 8;
            var maximumPasswordLength = 20;
            var listError = new List<string>();
            var output = new OutputGetLogin();

            //Validar se senha entra na regra de min e max
            if (password.Length < minimumPasswordLength || password.Length > maximumPasswordLength)
            {
                listError.Add("Senha nao se encaixa no padrão do sistema");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            //Validar se email é vazio ou null
            if (string.IsNullOrEmpty(email))
            {
                listError.Add("Campo email não pode ser vazio");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }
            return output;
        }

        public OutputGetLogin ValidateInsertion(Guid Id, string email, string password, string name)
        {
            var minimumPasswordLength = 8;
            var maximumPasswordLength = 20;
            var listError = new List<string>();
            var output = new OutputGetLogin();

            //Validar se senha entra na regra de min e max
            if (password.Length < minimumPasswordLength || password.Length > maximumPasswordLength)
            {
                listError.Add("Senha nao se encaixa no padrão do sistema");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            //Validar se email é vazio ou null
            if (string.IsNullOrEmpty(email))
            {
                listError.Add("Campo email não pode ser vazio");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            if (string.IsNullOrEmpty(name))
            {
                listError.Add("Campo nome não pode ser vazio");
                output.AddError(ReturnCode.failed, listError.ToArray());
            }

            output.ReturnCode = ReturnCode.exito;

            return output;
        }
    }
}
