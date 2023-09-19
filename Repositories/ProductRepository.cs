using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Login;
using System.Data;
using Dapper;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionString");
        }

        public Task<OutputProduct> DiscardProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> EditProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> FindProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> SaveProduct(InputProduct input)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM [dbo].[User] WHERE Email = @Email";
                var entity = dbConnection.QuerySingleOrDefault<LoginEntity>(sql, new { Email = input}); ;

                return null;
            }
        }
    }
}
