using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationService
    {
        Task<OutputGetLogin> Validatelogin(InputLogin input);
    }
}