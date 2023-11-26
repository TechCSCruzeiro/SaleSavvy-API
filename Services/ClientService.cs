using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.Client.Input;
using SaleSavvy_API.Models.Client.Output;
using SaleSavvy_API.Models.Validates;

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

                    return output;
                }

                var getClient = await _clientRepository.GetClientBy(input.Email, input.Name);

                if (getClient != null)
                {
                    var validateClient = new ValidateClient().ValidateDuplicate(input, getClient);

                    if (validateClient.ReturnCode == ReturnCode.failed)
                    {
                        output.AddError(validateClient.Error.MenssageError);

                        return output;
                    }
                }
            }

            output = await _clientRepository.AddClient(input, id);

            if (output.ReturnCode == ReturnCode.failed)
            {
                output.AddError(output.Error.MenssageError);
            }

            return output;
        }


        public async Task<Client> GetClient(Guid clientId)
        {
            var output = await _clientRepository.GetClientById(clientId);

            if (output != null)
            {
                return output; 
            }

            return null;
        }

        public async Task<List<Client>> GetListClient(Guid userId)
        {
            var validateUser = await _userRepository.GetUserById(userId);

            if (validateUser == null)
            {
                throw new ArgumentException("Usuario não encontrado");
            }

            var output = await _clientRepository.GetAll(userId);

            if (output == null)
            {
                throw new ArgumentException("Clientes deste usuario não encontrado");
            }

            return output;
        }
    }
}
