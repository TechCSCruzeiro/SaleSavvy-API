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
using SaleSavvy_API.Models.UpdateUser;
using System.Collections.Generic;

namespace SaleSavvy_API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private IConfiguration _configuracoes;

        public ProductRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<OutputProduct> DiscardProduct(Guid productId)
        {
            var output = new OutputProduct();

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                dbConnection.Open();
                try
                {
                    string sql = @"UPDATE ""product"" 
                                  SET ""IsActive"" = false
                                  WHERE ""Id"" = @Id";

                    await dbConnection.ExecuteAsync(sql, new { Id = productId });

                    output.ReturnCode = ReturnCode.exito;
                }
                catch (Exception ex)
                {
                    var listError = new List<string>();
                    listError.Add("Erro ao deletar produto: " + $"{ex}");
                    output.AddError(listError.ToArray());
                }

                return output;
            }
        }

        public async Task<OutputProduct> EditProduct(Product product)
        {
            var output = new OutputProduct();

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                dbConnection.Open();

                try
                {

                    string sql = @"UPDATE product
	                        SET ""Name"" = @Name,
                                ""Description"" = @Description, 
                                ""Price"" = @Price, 
                                ""Quantity"" = @Quantity, 
                                ""IsActive"" = @IsActive
	                        WHERE ""Id"" = @Id;";

                    await dbConnection.ExecuteAsync(sql,
                        new { Id = product.Id,
                              Name = product.Name,
                              Description = product.Description,
                              Price = product.Price,
                              Quantity = product.Quantity,
                              IsActive = product.IsActive
                        });

                    output.ReturnCode = ReturnCode.exito;
                }
                catch (SqlException ex)
                {
                    var listError = new List<string>();
                    listError.Add("Erro ao deletar produto: " + $"{ex}");
                    output.AddError(listError.ToArray());
                }

                return output;
            }
        }

        public async Task<ProductDto[]> FindProduct(Guid id)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {
                var products = await conexao.QueryAsync<Product>(
                    @"SELECT ""product"".* FROM ""product""
                        INNER JOIN ""user_"" ON ""product"".""UserID"" = ""user_"".""Id"" 
                        WHERE ""user_"".""Id"" = @UserId
                        AND ""product"".""IsActive"" = true;",
                    new
                    {
                        UserId = id
                    });

                if (products.Count() > 0)
                {
                    var output = new List<ProductDto>();
                    foreach (var product in products)
                    {
                        var prod = new ProductDto(product);
                        output.Add(prod);
                    }

                    return output.ToArray();
                }
                throw new ArgumentException("NÃO EXISTE PRODUTOS PARA ESSE USUÁRIO");
            }
        }

        public async Task<ProductDto> FindProductById(Guid productId)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
               _configuracoes.GetConnectionString("PostgresConnection")))
            {

                var id = Guid.Parse(productId.ToString());
                string selectUser = "SELECT * FROM \"product\" WHERE \"Id\" = @Id";
                var selectProduct = await conexao.QueryAsync<Product>(selectUser, new { Id = productId });


                if (selectProduct.Count() > 0)
                {
                    var output = new ProductDto(selectProduct.FirstOrDefault());

                    return output;
                }

                return null;
            }
        }

        public async Task<OutputProduct> SaveProduct(InputSaveProduct input, Guid id)
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
                            Id = id,
                            UserId = input.UserId,
                            Name = input.Name,
                            Description = input.Description,
                            Price = input.Price,
                            Quantity = input.Quantity,
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
