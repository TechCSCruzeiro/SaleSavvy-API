using SaleSavvy_API.Models;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.UpdateUser;

namespace SaleSavvy_API.Interface
{
    public interface IUserRepository
    {
        Task<GetUserEntity[]> All();
        Task<OutputUpdateUser> DeleteEmployee(string id);
       public Task<OutputUpdateUser> UpdateEmployee(InputUpdateUser input);
    }
}
