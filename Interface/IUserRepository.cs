using SaleSavvy_API.Models;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;

namespace SaleSavvy_API.Interface
{
    public interface IUserRepository
    {
        Task<GetUserEntity[]> All();
        Task<ReturnCode> DeleteEmployee(string id);
    }
}
