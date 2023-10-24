using SaleSavvy_API.Models.Client;

namespace SaleSavvy_API.Interface
{
    public interface IClientRepository
    {
        Task<OutputClient> AddClient (InputClient customer, Guid id);

        Task<OutputGetClient> GetClientById(Guid id);
    }
}
