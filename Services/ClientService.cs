using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client;

namespace SaleSavvy_API.Services
{
    public class ClientService : IClientService
    {
        IClientRepository _clientRepository;
        IUserRepository _userRepository;
        public ClientService(IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task<OutputClient> RegisterClient(InputClient input)
        {
            var output = new OutputClient();
            var validate = new ValidateClient().Validate(input);
            var listError = new List<string>();
            var id = Guid.NewGuid();

            if (validate.ReturnCode == ReturnCode.exito)
            {
                var validateUser = await _userRepository.GetUserById(input.UserID);

                if (validateUser == null)
                {
                    listError.Add("Usuario não encontrado");
                    output.AddError(listError.ToArray());
                }

                var validateId = await _clientRepository.GetClientById(id);

                if (validateId != null)
                {
                    listError.Add("Id de usuario ja existe");
                    output.AddError(listError.ToArray());

                    return output;
                }

                output = await _clientRepository.AddClient(input, id);
            }
            return output;
        }
    }
}
