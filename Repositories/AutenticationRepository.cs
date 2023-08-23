using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using System.Data;

namespace SaleSavvy_API.Repositories
{
    public class AutenticationRepository : IAutenticationRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OutputLogin> GetLogin(InputLogin input, Guid id)
        {
            var result = new OutputLogin();

            try
            {
                result.ReturnCode = ReturnCode.exito;
                return result;

            }catch(Exception ex)
            {
                result.ReturnCode = ReturnCode.failed;
                return result;
            }
        }
    }
}
