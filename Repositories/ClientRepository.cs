using Dapper;
using Newtonsoft.Json;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.GetUser.Entity;

namespace SaleSavvy_API.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private IConfiguration _configuracoes;

        public ClientRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<OutputClient> AddClient(InputClient customer, Guid id)
        {
            var output = new OutputClient();

            string AddressJson = JsonConvert.SerializeObject(customer.Address);

            using (NpgsqlConnection connection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                try
                {
                    string sql = @"INSERT INTO public.client(""Id"", ""Name"", ""Email"", ""Phone"", ""Address"", ""UserId"")
                            VALUES (@Id, @Name, @Email, @Phone, @Address, @UserId);";

                    var entity = await connection.QueryAsync<OutputGetClient>(sql,
                        new
                        {
                            Id = id,
                            Name = customer.Name,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            Address = AddressJson,
                            UserId = customer.UserID
                        });

                    if (entity.Equals(0))
                    {
                        var listError = new List<string>();
                        listError.Add("Produto já existe");
                        output.AddError(listError.ToArray());
                    }
                    else
                    {
                        output.ReturnCode = Models.ReturnCode.exito;
                    }
                }
                catch (Exception ex)
                {
                    var listError = new List<string>();
                    listError.Add($"Erro PostgreSQL: {ex.Message}");
                    output.AddError(listError.ToArray());
                }
                return output;
            }
        }

        public async Task<OutputGetClient> GetClientById(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
              _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = "SELECT * FROM \"client\"WHERE \"Id\" = @Id";

                var entity = await connection.QueryAsync<OutputGetClient>(sql, new { Id = id });

                if (entity != null)
                {
                    return entity.FirstOrDefault();
                }

                return null;

            }
        }
    }
}
