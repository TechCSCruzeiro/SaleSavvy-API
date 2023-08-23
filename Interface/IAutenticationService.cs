using SaleSavvy_API.Models;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationService
    {
        Task<OutputLogin> Validatelogin(InputLogin input);
    }
}
