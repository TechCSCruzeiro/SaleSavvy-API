using SaleSavvy_API.Models.Sales.Input;
using SaleSavvy_API.Models.Sales.Output;

namespace SaleSavvy_API.Interface
{
    public interface ISalesService
    {
        Task<OutputSales> TransactionSales(InputSales input) ;
    }
}
