using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Input;

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
    }
}