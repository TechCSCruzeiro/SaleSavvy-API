using SaleSavvy_API.Models.Register;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.User.Entity;
using SaleSavvy_API.Models.User.Input;
using SaleSavvy_API.Models.User.Output;

namespace SaleSavvy_API.Interface
{
    public interface IUserRepository
    {
        Task<GetUserEntity[]> All();
        Task<OutputUpdateUser> DeleteEmployee(string id);
        Task<OutputUpdateUser> UpdateEmployee(InputUpdateUser input);
        Task<Register> InsertRegister(InputRegister input);
        Task<GetUserEntity> GetUserById(Guid userId);
    }
}
