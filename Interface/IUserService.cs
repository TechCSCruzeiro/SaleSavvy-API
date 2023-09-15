using SaleSavvy_API.Models;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;

namespace SaleSavvy_API.Interface
{
    public interface IUserService
    {
        Task<GetUserEntity[]> GetListUser();
        Task<ReturnCode> DeleteUser(string id);
    }
}
