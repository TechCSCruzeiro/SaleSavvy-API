using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Register;
using SaleSavvy_API.Models.Register.Input;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationRepository
    {
        /// <summary>
        /// Validar Login no banco
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Login> GetLogin(InputLogin input);

        /// <summary>
        /// Cadastrar Usuario
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Register> InsertRegister(InputRegister input);
    }
}