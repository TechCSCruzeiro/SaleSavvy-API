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
        
        
        public async Task<ReturnCode> DeleteEmployee(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                try
                {
                    string sql = "DELETE FROM [dbo].[User] WHERE Id = @Id";
                    await dbConnection.QueryAsync(sql, new {Id = id});

                    return ReturnCode.exito;
                }catch (Exception ex)
                {
                    throw new ArgumentException("Error Ao deletar do banco: \n" + ex);
                }

                
                
            };
        }
    }
}
