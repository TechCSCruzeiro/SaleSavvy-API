using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Input;

namespace SaleSavvy_API.Services
{
    public class AutenticationService : IAutenticationService
    {
        IAutenticationRepository _autenticationRepository;
        public AutenticationService(IAutenticationRepository autenticationRepository)
        {
            _autenticationRepository = autenticationRepository;
        }

        /// <summary>
        /// Validação de Login
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>n
        public async Task<OutputGetLogin> Validatelogin(InputLogin input)
        {
            var minimumPasswordLength = 8;
            var maximumPasswordLength = 20;
            var validate = new Validate();
            var output = new OutputGetLogin();

            //Validar se senha entra na regra de min e max
            if (input.Password.Length < minimumPasswordLength || input.Password.Length > maximumPasswordLength)
            {
                throw new ArgumentException("Quantidade de Caracteres invalido", "Min:" + minimumPasswordLength + "Max:" + maximumPasswordLength);
            }

            //Validar se email é vazio ou null
            if (string.IsNullOrEmpty(input.Email))
            {
                throw new ArgumentException("Campo de Login Vazio");
            }

            //Consultar email na base
            var getLogin = await _autenticationRepository.GetLogin(input);

            //Validar returnCode da consulta 
            if (getLogin.ReturnCode.Equals(ReturnCode.failed))
            {
                //Retornar error da consulta
                output.AddError(getLogin.ReturnCode, getLogin.Error.MenssageError);
            }
            else
            {
                //Realizar comparacao de senha inserida com senha da base
                output = validate.ValidateLogin(input, getLogin);

                //Retornar Error caso a compracao de error
                if (output.ReturnCode.Equals(ReturnCode.failed))
                {
                    output.AddError(output.ReturnCode, output.Error.MenssageError);
                }
            }

            return output;
        }

    }
}