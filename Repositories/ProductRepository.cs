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

        public async Task<OutputProduct> SaveProduct(InputProduct input)
        {
            var output = new OutputProduct();

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string selectUser = "SELECT * FROM [USER] WHERE Id = @Id";
                var userCount = await dbConnection.QueryAsync<LoginEntity>(selectUser, new { Id = input.UserId });

                if (userCount.Count() > 0)
                {
                    try
                    {
                        var inserProduct = await dbConnection.QueryAsync<LoginEntity>(@"
                             INSERT INTO [dbo].[Product]
                             ([Id]
                             ,[UserID]
                             ,[Name]
                             ,[Description]
                             ,[Price]
                             ,[Quantity]
                             ,[CreationDate])
                             SELECT
                             NEWID(),
                             @UserID,
                             @Name,
                             @Description,
                             @Price,
                             @Quantity,
                             GETDATE()
                             WHERE NOT EXISTS (
                                 SELECT 1
                                 FROM [dbo].[Product]
                                 WHERE [Name] = @Name
                             );",
                             new
                             {
                                 UserId = input.UserId,
                                 Id = input.Product.Id,
                                 Name = input.Product.Name,
                                 Description = input.Product.Description,
                                 Price = input.Product.Price,
                                 Quantity = input.Product.Quantity,
                             });
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }

                }

                var listError = new List<string>();
                listError.Add("Erro ao inserir o produto no banco");

                output.AddError(listError.ToArray());

                return output;
            }
        }
    }
}
