using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Register;
using SaleSavvy_API.Models.Register.Input;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationRepository
    {
        Task<Login> GetLogin(InputLogin input);
    }
}