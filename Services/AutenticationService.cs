using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;

namespace SaleSavvy_API.Services
{
    public class AutenticationService : IAutenticationService
    {
        public OutputLogin Validatelogin(InputLogin input)
        {
            var output = new OutputLogin();
            output.StatusLogin = true;

            return output;
        }
    }
}
