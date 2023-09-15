using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Login.Output;
using SaleSavvy_API.Models.Register;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register.Output;

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
            var validate = new Validate();
            var output = new OutputGetLogin();
            var validateLogin = new ValidateLogin();

            var errorInstance = validateLogin.ValidateInsertion(input);

            if (errorInstance.Error != null && errorInstance.Error.MenssageError.Length > 0)
            {
                output = errorInstance;
            } 
            else 
            {
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
            }

            return output;
        }

        public async Task<OutputRegister> ValidateRegister(InputRegister input)
        {
            var output = new OutputRegister(); 
            var validate = new ValidateRegister();
            var validateRegister = validate.ValidateInsertRegister(input);

            if (validateRegister.Error != null && validateRegister.Error.MenssageError.Length > 0)
            {
                output.Error = validateRegister.Error;
            }
            else
            {
                var insertRegister = await _autenticationRepository.InsertRegister(input);

                if (insertRegister.ReturnCode.Equals(ReturnCode.exito)) 
                {
                    output.ReturnCode = insertRegister.ReturnCode;

                }
                else
                {
                    var listError = new List<string>();

                    listError.Add(insertRegister.ErrorMessage);

                    output.AddError(insertRegister.ReturnCode, listError.ToArray());
                }
            }

            return output;
        }
    }
}