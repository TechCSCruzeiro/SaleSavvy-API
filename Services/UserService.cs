using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;

namespace SaleSavvy_API.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserEntity[]> GetListUser()
        {
            return await _userRepository.All();
        }       
        
        public async Task<ReturnCode> DeleteUser(string id)
        {
            return await _userRepository.DeleteEmployee(id);
        }
    }
}
