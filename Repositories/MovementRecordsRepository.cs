using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Models.Products;
using System.Data;

namespace SaleSavvy_API.Repositories
{
    public class MovementRecordsRepository : IMovementRecordsRepository
    {
        private IConfiguration _configuracoes;

        public MovementRecordsRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<OutputProduct> SaveRecord(StatusMovementRecords status, InputProduct product)
        {
            var output = new OutputProduct();

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
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
                ""NewValue"",
                ""OldValue"")
                SELECT
                uuid_generate_v4(),
                @ProductId,
                @Status,
                NOW(),
                (SELECT ""Quantity"" FROM ""product"" WHERE ""Id"" = @ProductId),
                @MovementQuantity,
                @NewValue,
                @OldValue ";

                    if (status != StatusMovementRecords.Entrada)
                    {
                        sqlQueryInsert += @"WHERE ((SELECT ""Quantity"" 
                                            FROM ""product"" 
                                            WHERE ""Id"" = @ProductId) >= @MovementQuantity);"; ;
                    }

                    var inserProduct = await dbConnection.ExecuteAsync(sqlQueryInsert,
                        new
                        {
                            ProductId = product.Product.Id,
                            Status = status.ToString(),
                            MovementQuantity = product.Product.Quantity,
                            OldValue = product.OldPrice != null ? product.OldPrice : 0m,
                            NewValue = status == StatusMovementRecords.AlteracaoValor ? product.Product.Price : 0m,
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

        public async Task<List<OutputRecordStock>> GetStockReportInfo(InputRecordStock input)
        {
            var result = new OutputRecordStock();
            var output = new List<OutputRecordStock>();

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                dbConnection.Open();

                string sqlQueryInsert = @"
                    SELECT
                        product.""Name"",
                        movement_records.""Status"",
                        movement_records.""DateMovement"",
                        movement_records.""CurrentQuantity"",
                        movement_records.""MovementQuantity"",
                        movement_records.""NewValue"",
                        movement_records.""OldValue""
                    FROM
                        movement_records
                    LEFT JOIN
                        product
                    ON product.""Id"" = movement_records.""ProductId""            
                    WHERE
                        movement_records.""DateMovement"" >= @StartDate AND movement_records.""DateMovement"" <= @EndDate;
                    ";

                var recordInfo = await dbConnection.QueryAsync<RecordEntity>(sqlQueryInsert,
                    new
                    {
                        StartDate = input.StartDate,
                        EndDate = input.EndDate
                    });

                if (recordInfo.Count() == 0)
                {
                    throw new ArgumentException("Erro ao encontrar dados de estoque");
                }
                else
                {
                    foreach (var record in recordInfo)
                    {
                        var stockInfo = new OutputRecordStock(record);
                        output.Add(stockInfo);
                    }

                    return output;
                }

            }
        }

    }
}
