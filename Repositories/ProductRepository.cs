using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Login;
using System.Data;
using Dapper;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Models;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SaleSavvy_API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private IConfiguration _configuracoes;

        public ProductRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public Task<OutputProduct> DiscardProduct()
        {
            throw new NotImplementedException();
        }

        public Task<OutputProduct> EditProduct()
        {
            throw new NotImplementedException();
        }
        public async Task<Product[]> FindProduct(Guid id)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                var products = await conexao.QueryAsync<Product>(
                    @"SELECT ""product"".* FROM ""product""
                        INNER JOIN ""user_"" ON ""product"".""UserID"" = ""user_"".""Id"" 
                        WHERE ""user_"".""Id"" = @UserId",
                    new
                    {
                        UserId = id
                    });

                if (products.Count() > 0)
                {
                    return products.ToArray();
                }
                throw new ArgumentException("NÃO EXISTE PRODUTOS PARA ESSE USUÁRIO");
            }
        }

        public async Task<OutputProduct> SaveProduct(InputProduct input)
        {
            var output = new OutputProduct();

            using (NpgsqlConnection connection = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                connection.Open();

                string selectUser = "SELECT * FROM \"user_\" WHERE \"Id\" = @Id";
                var userCount = await connection.QueryAsync<LoginEntity>(selectUser, new { Id = input.UserId });

                if (userCount.Count() == 0)
                {
                    var listError = new List<string>();
                    listError.Add("Usuário não encontrado");
                    output.AddError(listError.ToArray());
                    return output;
                }

                try
                {
                    var insertProduct = await connection.ExecuteAsync(@"
                        INSERT INTO ""product""
                            (""Id"",
                            ""UserID"",
                            ""Name"",
                            ""Description"",
                            ""Price"",
                            ""Quantity"",
                            ""CreationDate"")
                        SELECT
                            @Id,
                            @UserID,
                            @Name,
                            @Description,
                            @Price,
                            @Quantity,
                            NOW()
                        WHERE NOT EXISTS (
                            SELECT 1
                            FROM ""product""
                            WHERE ""Name"" = @Name
                        );",
                        new
                        {
                            Id = input.Product.Id,
                            UserId = input.UserId,
                            Name = input.Product.Name,
                            Description = input.Product.Description,
                            Price = input.Product.Price,
                            Quantity = input.Product.Quantity,
                        });

                    if (insertProduct.Equals(0))
                    {
                        var listError = new List<string>();
                        listError.Add("Produto já existe");
                        output.AddError(listError.ToArray());
                    }
                    else
                    {
                        output.ReturnCode = Models.ReturnCode.exito;
                        return output;
                    }
                }
                catch (NpgsqlException ex)
                {
                    var listError = new List<string>();
                    listError.Add($"Erro PostgreSQL: {ex.Message}");
                    output.AddError(listError.ToArray());
                }

                return output;
            }
        }
    }
}
