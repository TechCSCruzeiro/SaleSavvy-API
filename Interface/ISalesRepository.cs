using SaleSavvy_API.Models.Sales.Input;
using SaleSavvy_API.Models.Sales.Output;

namespace SaleSavvy_API.Interface
{
    public interface ISalesRepository
    {
        Task<OutputSales> InsertTransaction(InputSales input, Guid idPayment);
        Task<OutputSales> InsertItemsSales(Guid transactionId, ProductSales product);
    }
}
