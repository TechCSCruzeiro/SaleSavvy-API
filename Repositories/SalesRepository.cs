using Dapper;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Sales.Input;
using SaleSavvy_API.Models.Sales.Output;

namespace SaleSavvy_API.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private IConfiguration _configuracoes;

        public SalesRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<OutputSales> InsertTransaction(InputSales input, Guid paymentId)
        {
            var output = new OutputSales();
            var transactionId = Guid.NewGuid();

            using (NpgsqlConnection connection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = @"
                    INSERT INTO transaction(
                      ""Id"", ""DateTransaction"", ""TotalSales"", ""ClientId"", ""PaymentId"", ""Parcel"")
                         VALUES (@Id, @DateTransaction, @TotalSales, @ClientId, @PaymentId, @Parcel);";

                var entity = await connection.QueryAsync(sql,
                  new
                  {
                      Id = transactionId,
                      DateTransaction = DateTime.Now,
                      TotalSales = input.TotalPrice,
                      ClientId = input.ClientId,
                      PaymentId = paymentId,
                      Parcel = input.QuantityParcel != null ? input.QuantityParcel : 0,
                  }); ;

                if (entity == null)
                {
                    var listError = new List<string>();
                    listError.Add("Error ao realizar transação na base");
                    output.AddError(listError.ToArray());
                }

                output.ReturnCode = ReturnCode.exito;
                output.TransactionId = transactionId;
                return output;
            }
        }

        public async Task<OutputSales> InsertItemsSales(Guid transactionId, ProductSales product)
        {
            var output = new OutputSales();

            using (NpgsqlConnection connection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = @"
                 INSERT INTO sales_items(
                   ""TransactionId"", ""ProductId"", ""Quantity"", ""UnitPrice"")
                       VALUES (@TransactionId, @ProductId, @Quantity, @UnitPrice);";

                var entity = await connection.QueryAsync(sql,
                  new
                  {
                      TransactionId = transactionId,
                      ProductId = product.Id,
                      Quantity = product.Quantity,
                      UnitPrice = product.Price
                  });

                if (entity == null)
                {
                    var listError = new List<string>();
                    listError.Add("Error ao realizar transação na base");
                    output.AddError(listError.ToArray());
                }

                output.ReturnCode = ReturnCode.exito;
                output.TransactionId = transactionId;
                return output;
            }
        }
    }
}
