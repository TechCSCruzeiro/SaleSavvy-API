using SaleSavvy_API.Models.Client;

namespace SaleSavvy_API.Interface
{
    public interface IClientService
    {
        Task<OutputClient> RegisterClient(InputClient input);
    }
}
