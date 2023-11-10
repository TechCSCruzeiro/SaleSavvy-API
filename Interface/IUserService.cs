using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register.Output;
using SaleSavvy_API.Models.User.Entity;
using SaleSavvy_API.Models.User.Input;
using SaleSavvy_API.Models.User.Output;

namespace SaleSavvy_API.Interface
{
    public interface IUserService
    {
        Task<GetUserEntity[]> GetListUser();
        Task<OutputUpdateUser> DeleteUser(string id);
        Task<OutputUpdateUser> ModifyUserData(InputUpdateUser input);
        Task<OutputRegister> ValidateRegister(InputRegister input);
        Task<GetUserEntity> SearchUserById(Guid userId);
    }
}
