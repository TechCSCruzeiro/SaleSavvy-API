using SaleSavvy_API.Models;

namespace SaleSavvy_API.Interface
{
    public interface IAutenticationService
    {
        OutputLogin Validatelogin(InputLogin input);
    }
}
