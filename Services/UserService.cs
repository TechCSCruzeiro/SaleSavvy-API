using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register.Output;
using SaleSavvy_API.Models.User.Entity;
using SaleSavvy_API.Models.User.Input;
using SaleSavvy_API.Models.User.Output;
using SaleSavvy_API.Models.Validates;

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

        public async Task<OutputRegister> ValidateRegister(InputRegister input)
        {
            var output = new OutputRegister();
            var validate = new ValidateRegister();
            var validateRegister = validate.ValidateInsertRegister(input);

            if (validateRegister.Error != null && validateRegister.Error.MenssageError.Length > 0)
            {
                output.Error = validateRegister.Error;
            }
            else
            {
                var insertRegister = await _userRepository.InsertRegister(input);

                if (insertRegister.ReturnCode.Equals(ReturnCode.exito))
                {
                    output.ReturnCode = insertRegister.ReturnCode;

                }
                else
                {
                    var listError = new List<string>();

                    listError.Add(insertRegister.ErrorMessage);

                    output.AddError(insertRegister.ReturnCode, listError.ToArray());
                }
            }

            return output;
        }

        public async Task<GetUserEntity> SearchUserById(Guid userId)
        {
            return await _userRepository.GetUserById(userId);
        }
    }
}
