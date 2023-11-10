using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.Client.Input;
using SaleSavvy_API.Models.Client.Output;

namespace SaleSavvy_API.Interface
{
    public interface IClientService
    {
        Task<OutputClient> RegisterClient(InputClient input);
        Task<OutputGetClient> GetClient(Guid clientId);
        Task<List<Client>> GetListClient(Guid userId);
    }
}
