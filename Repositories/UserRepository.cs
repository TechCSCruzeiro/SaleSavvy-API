using Microsoft.Data.SqlClient;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Login;
using System.Data;
using Dapper;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;
using System.Diagnostics.Eventing.Reader;
using SaleSavvy_API.Models;
using System.Data.Common;
using SaleSavvy_API.Models.UpdateUser;

namespace SaleSavvy_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionString");
        }

        public async Task<GetUserEntity[]> All()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM [dbo].[User]";
                var entity = await dbConnection.QueryAsync<GetUserEntity>(sql);

                return entity.ToArray();
            };
        }


        public async Task<OutputUpdateUser> DeleteEmployee(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                try
                {

                    string select = "SELECT * FROM [dbo].[User] Where Id = @Id";
                    var resultSelect = await dbConnection.QueryAsync<LoginEntity>(select, new{  Id = id });

                    if (resultSelect.Count() != 0)
                    {
                        string sql = "DELETE FROM [dbo].[User] WHERE Id = @Id";
                        await dbConnection.QueryAsync(sql, new { Id = id });


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
                    throw new ArgumentException("Error Ao deletar do banco: \n" + ex);
                }



            };
        }

        public async Task<OutputUpdateUser> UpdateEmployee(InputUpdateUser input)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    string select = "SELECT * FROM [dbo].[User] Where Id = @Id";
                    var resultSelect = await dbConnection.QueryAsync<LoginEntity>(select,
                    new
                    {
                        Id = input.Id,
                        Email = input.Email,
                        Password = input.Password,
                        Name = input.Name
                    });


                    if (resultSelect.Count() != 0)
                    {
                        string sql = "UPDATE [dbo].[User] SET [Email] = @Email, [Password] = @Password, [Name] = @Name WHERE Id = @Id";
                        await dbConnection.QueryAsync(sql,
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
                catch (Exception ex)
                {
                    throw new ArgumentException("Error Ao deletar do banco: \n" + ex);
                }
            }
        }
    }
}
