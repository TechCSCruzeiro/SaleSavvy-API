using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.Client.Input;
using SaleSavvy_API.Models.Client.Output;

namespace SaleSavvy_API.Interface
{
    public interface IClientRepository
    {
        Task<OutputClient> AddClient (InputClient customer, Guid id);

        Task<Client> GetClientById(Guid id);
        Task<Client> GetClientBy(string email, string name);
        Task<List<Client>> GetAll(Guid userId);
    }
}
