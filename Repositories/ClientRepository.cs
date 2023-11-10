using Dapper;
using Newtonsoft.Json;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.Client.Entity;
using SaleSavvy_API.Models.Client.Input;
using SaleSavvy_API.Models.Client.Output;

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
                        listError.Add("Cliente já existe");
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

        public async Task<List<Client>> GetAll(Guid userId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = "SELECT * FROM \"client\" WHERE \"UserId\" = @UserId";

                var entities = await connection.QueryAsync<ClientEntity>(sql, new { UserId = userId });

                var clients = entities.Select(clientInfo => new Client
                {
                    Name = clientInfo.Name,
                    Phone = clientInfo.Phone,
                    Email = clientInfo.Email,
                    Id = clientInfo.Id,
                    UserId = clientInfo.UserId,
                    Address = JsonConvert.DeserializeObject<Address>(clientInfo.Address)
                }).ToList();

                return clients;
            }
        }

        public async Task<Client> GetClientBy(string email, string name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = @"
                    SELECT *
                    FROM ""client""
                    WHERE ""Name"" = @Name OR ""Email"" = @Email";

                var entity = await connection.QueryAsync<ClientEntity>(sql, new { Name = name, Email = email});

                if(entity.Count() != 0)
                {
                    var adress = JsonConvert.DeserializeObject<Address>(entity.FirstOrDefault().Address) ;

                    var output = new Client
                    {
                        Name = entity.FirstOrDefault().Name,
                        Phone = entity.FirstOrDefault().Phone,
                        Email = entity.FirstOrDefault().Email,
                        Id = entity.FirstOrDefault().Id,
                        UserId = entity.FirstOrDefault().UserId,
                        Address = adress
                    };
                }

                return null;
            }
        }

    }
}
