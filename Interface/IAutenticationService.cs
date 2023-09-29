using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Login.Output;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register.Output;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationService
    {
        Task<OutputGetLogin> Validatelogin(InputLogin input);
    }
}