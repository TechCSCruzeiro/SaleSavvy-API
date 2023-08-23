using SaleSavvy_API.Models;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationRepository
    {
        /// <summary>
        /// Validar Login no banco
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OutputLogin> GetLogin(InputLogin input, Guid id);
    }
}
