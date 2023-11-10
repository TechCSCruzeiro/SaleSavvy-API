using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Login.Output;
using SaleSavvy_API.Models.Validates;

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
        /// <returns></returns>
        public async Task<OutputGetLogin> Validatelogin(InputLogin input)
        {
            var validate = new Validate();
            var output = new OutputGetLogin();
            var validateLogin = new ValidateLogin();

            var errorInstance = validateLogin.ValidateInsertion(
                input.ID,
                input.Email,
                input.Password
                );

            if (errorInstance.Error != null && errorInstance.Error.MenssageError.Length > 0)
            {
                output = errorInstance;
            }
            else
            {
                //Consultar email na base
                var getLogin = await _autenticationRepository.GetLogin(input);

                //Validar returnCode da consulta 
                if (getLogin == null)
                {
                    var listError = new List<string>();
                    listError.Add("Cadastro não encontrado");

                    //Retornar error da consulta
                    output.AddError(ReturnCode.failed, listError.ToArray());
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
            }

            return output;
        }
    }
}