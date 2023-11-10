using Dapper;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Register;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.User.Entity;
using SaleSavvy_API.Models.User.Input;
using SaleSavvy_API.Models.User.Output;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SaleSavvy_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _configuracoes;

        public UserRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<GetUserEntity[]> All()
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = "SELECT * FROM \"user_\"";

                var entity = await conexao.QueryAsync<GetUserEntity>(sql);

                return entity.ToArray();
            }
        }

        public async Task<OutputUpdateUser> DeleteEmployee(string id)
        {
            using (NpgsqlConnection dbConnection = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                dbConnection.Open();
                try
                {
                    Guid userId = Guid.Parse(id); // Converte a string em um GUID

                    string select = "SELECT * FROM \"user_\" WHERE \"Id\" = @Id";
                    var resultSelect = await dbConnection.QueryAsync<LoginEntity>(select, new { Id = userId });

                    if (resultSelect.Count() != 0)
                    {
                        string sql = "DELETE FROM \"user_\" WHERE \"Id\" = @Id";
                        await dbConnection.ExecuteAsync(sql, new { Id = userId });

                        return new OutputUpdateUser(ReturnCode.exito);
                    }
                    else
                    {
                        var output = new OutputUpdateUser();
                        var listError = new List<string>();

                        listError.Add("Id não encontrado na base");
                        output.AddError(ReturnCode.failed, listError.ToArray());

                        return output;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Erro Ao deletar do banco: \n" + ex);
                }
            }
        }

        public async Task<OutputUpdateUser> UpdateEmployee(InputUpdateUser input)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                try
                {
                    string select = "SELECT * FROM \"user_\" WHERE \"Id\" = @Id";
                    var resultSelect = await conexao.QueryAsync<LoginEntity>(select,
                        new
                        {
                            Id = input.Id,
                            Email = input.Email,
                            Password = input.Password,
                            Name = input.Name
                        });

                    if (resultSelect.Count() != 0)
                    {
                        string sql = "UPDATE \"user_\" SET \"Email\" = @Email, \"Password\" = @Password, \"Name\" = @Name WHERE \"Id\" = @Id";
                        await conexao.ExecuteAsync(sql,
                            new
                            {
                                Id = input.Id,
                                Email = input.Email,
                                Password = input.Password,
                                Name = input.Name
                            });

                        return new OutputUpdateUser(ReturnCode.exito);
                    }
                    else
                    {
                        var output = new OutputUpdateUser();
                        var listError = new List<string>();

                        listError.Add("Id não encontrado na base");
                        output.AddError(ReturnCode.failed, listError.ToArray());

                        return output;
                    }
                }
                catch (NpgsqlException ex)
                {
                    throw new ArgumentException("Erro ao atualizar no banco: \n" + ex);
                }
            }
        }

        public async Task<Register> InsertRegister(InputRegister input)
        {
            var output = new Register(ReturnCode.exito);

            using (NpgsqlConnection conexao = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string message;

                var consultUserMail = await conexao.QuerySingleOrDefaultAsync("SELECT * FROM \"user_\" WHERE \"Email\" = @Email",
                    new { Email = input.Email.ToLower() });

                if (consultUserMail != null)
                {
                    message = "Email já cadastrado";
                    return output = new Register(ReturnCode.failed, message);
                }

                var consultUserName = await conexao.QuerySingleOrDefaultAsync("SELECT * FROM \"user_\" WHERE \"Name\" = @Name",
                    new { Name = input.Name.ToLower() });

                if (consultUserName != null)
                {
                    message = "Nome já cadastrado";
                    return output = new Register(ReturnCode.failed, message);
                }

                try
                {
                    string insertRegister = "INSERT INTO \"user_\"(\"Id\",\"Email\",\"Password\",\"Name\") " +
                        "VALUES(@Id, @Email, @Password, @Name);";

                    var insert = await conexao.ExecuteAsync(insertRegister,
                        new
                        {
                            Id = Guid.NewGuid(),
                            Email = input.Email.ToLower(),
                            Password = input.Password,
                            Name = input.Name,
                        });

                    return output;
                }
                catch (NpgsqlException ex)
                {
                    message = ("Erro ao criar usuário no banco: " + ex.ErrorCode);
                    return output = new Register(ReturnCode.failed, message);
                }
            }
        }

        public async Task<GetUserEntity> GetUserById(Guid userId)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                string sql = "SELECT * FROM \"user_\"WHERE \"Id\" = @Id";

                var entity = await conexao.QueryAsync<GetUserEntity>(sql, new { Id = userId });

                if (entity != null)
                {
                    return entity.FirstOrDefault();
                }

                return null;

            }
        }

        public async Task<bool> ModificAdm(Guid userId, bool isAdm)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                try
                {
                    string sql = @"
                   UPDATE user_
                   SET ""IsAdm"" = true
                   WHERE ""Id"" = @UserId"
;

                    var entity = await conexao.ExecuteAsync(sql, new { UserId = userId });

                    return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Erro ao atualizar o tipo do usuario");
                }
            }
        }
    }
}
