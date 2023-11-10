using SaleSavvy_API.Models.Payment.Output;

namespace SaleSavvy_API.Interface
{
    public interface IPaymentRepository
    {
        Task<List<OutputPayment>> FindAll();
    }
}
