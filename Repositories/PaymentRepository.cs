using Dapper;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Payment.Entity;
using SaleSavvy_API.Models.Payment.Output;

namespace SaleSavvy_API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private IConfiguration _configuracoes;

        public PaymentRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<List<OutputPayment>> FindAll()
        {

            var output = new  List<OutputPayment>();

            using (NpgsqlConnection connection = new NpgsqlConnection(
              _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = "SELECT * FROM payment_methods";

                var entity = await connection.QueryAsync<EntityPayment>(sql);

                foreach(var payment in entity)
                {
                    var result = new OutputPayment();
                    
                    result.Id = payment.Id;
                    result.FormPayment = payment.Name;

                    output.Add(result);
                }

                return output;

            }
        }

    }
}
