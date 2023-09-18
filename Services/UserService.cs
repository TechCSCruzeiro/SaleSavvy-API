using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.UpdateUser;

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
        
        public async Task<OutputUpdateUser> DeleteUser(string id)
        {
            return await _userRepository.DeleteEmployee(id);
        }

        public async Task<OutputUpdateUser> ModifyUserData(InputUpdateUser input)
        {
            var validate = new ValidateLogin().ValidateInsertion(
                input.Id,
                input.Email,
                input.Password,
                input.Name
                );

            var output = new OutputUpdateUser();

            if (validate.ReturnCode != ReturnCode.exito)
            {
                output.AddError(validate.ReturnCode, validate.Error.MenssageError);

                return output;
            }

            return await _userRepository.UpdateEmployee(input);
        }
    }
}
