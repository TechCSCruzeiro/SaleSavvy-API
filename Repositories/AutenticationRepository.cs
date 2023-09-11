using Dapper;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Login.Input;
using System.Data;
using Microsoft.Data.SqlClient;


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
}