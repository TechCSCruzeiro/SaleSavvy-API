using Dapper;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Login.Input;
using System.Data;
using Microsoft.Data.SqlClient;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register;

namespace SaleSavvy_API.Repositories
{

    public class AutenticationRepository : IAutenticationRepository
    {

        private readonly string _connectionString;

        public AutenticationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionString");
        }

        public async Task<Login> GetLogin(InputLogin input)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM [dbo].[User] WHERE Email = @Email";
                var entity = dbConnection.QuerySingleOrDefault<LoginEntity>(sql, new { Email = input.Email.ToLower() }); ;

                if (entity != null)
                {
                    return new Login(entity);
                }
                else
                {
                    return new Login().AddError("Email Não Cadastrado");
                }
            };
        }

        public async Task<Register> InsertRegister(InputRegister input)
        {
            var output = new Register(ReturnCode.exito);
            
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string message;

                var consultUserMail = dbConnection.QuerySingleOrDefault("SELECT * FROM [dbo].[User] WHERE Email = @Email",
                  new {Email = input.Email.ToLower()});

                if (consultUserMail != null)
                {
                    message = "Email ja Cadastrado";
                    return output = new Register(ReturnCode.failed, message);
                }

                var consultUserName = dbConnection.QuerySingleOrDefault("SELECT * FROM [dbo].[User] WHERE [Name] = @Name",
                  new { Name = input.Name.ToLower() });

                if (consultUserName != null)
                {
                    message = "Nome ja Cadastrado";
                    return output = new Register(ReturnCode.failed, message);
                }

                try
                {

                    string insertRegister = "INSERT INTO [dbo].[User]([Id],[Email],[Password],[Name]) " +
                        "VALUES(@Id, @Email, @Password, @Name);";

                    var insert = await dbConnection.ExecuteAsync(insertRegister,
                        new
                        {
                            Id = Guid.NewGuid(),
                            Email = input.Email.ToLower(),
                            Password = input.Password,
                            Name = input.Name,
                        });

                    return output;

                }
                catch (SqlException ex)
                {
                    message = ("Error ao criar usuario no banco: " + ex.ErrorCode);
                    return output = new Register(ReturnCode.failed, message);
                }
            };
        }
    }
}
