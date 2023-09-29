using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Products;
using System.Data;

namespace SaleSavvy_API.Repositories
{
    public class MovementRecordsRepository : IMovementRecordsRepository
    {
        private readonly string _connectionString;

        public MovementRecordsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public async Task<OutputProduct> SaveRecord(StatusMovementRecords status, InputProduct product)
        {
            var output = new OutputProduct();

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                try
                {
                    string sqlQueryInsert = @"
                INSERT INTO ""movement_records""
                (""Id"",
                ""ProductId"",
                ""Status"",
                ""DateMovement"",
                ""CurrentQuantity"",
                ""MovementQuantity"",
                ""ChangedValue"")
                SELECT
                uuid_generate_v4(),
                @ProductId,
                @Status,
                NOW(),
                (SELECT ""Quantity"" FROM ""product"" WHERE ""Id"" = @ProductId),
                @MovementQuantity,
                @ChangedValue ";

                    if (status != StatusMovementRecords.Entrada)
                    {
                        sqlQueryInsert += @"WHERE ((SELECT ""Quantity"" 
                                            FROM ""movement_records"" 
                                            WHERE ""Id"" = @ProductId) >= @MovementQuantity);"; ;
                    }

                    var inserProduct = await dbConnection.ExecuteAsync(sqlQueryInsert,
                        new
                        {
                            ProductId = product.Product.Id,
                            Status = status.ToString(),
                            MovementQuantity = product.Product.Quantity,
                            ChangedValue = product.AlterValue != null ? product.AlterValue : null,
                        });

                    if (inserProduct == 0)
                    {
                        var listError = new List<string>();
                        listError.Add("Quantidade não permitida");
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
