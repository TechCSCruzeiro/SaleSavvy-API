using Dapper;
using Npgsql;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Models.MovementRecords.Entity;
using SaleSavvy_API.Models.MovementRecords.Input;
using SaleSavvy_API.Models.MovementRecords.Output;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Models.Products.Input;
using SaleSavvy_API.Models.Products.Output;
using SaleSavvy_API.Models.Sales.Entity;

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

        public async Task<List<OutputRecordStock>> GetStockMovementReportInfo(InputRecord input)
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
                        movement_records.""DateMovement"" >= @StartDate 
                        AND movement_records.""DateMovement"" <= @EndDate
                        AND product.""UserID"" = @UserID;
                    ";

                var recordInfo = await dbConnection.QueryAsync<RecordEntity>(sqlQueryInsert,
                    new
                    {
                        StartDate = input.StartDate,
                        EndDate = input.EndDate,
                        UserID = input.UserId
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

        public async Task<List<OutputRecordSales>> GetSallesReportInfo(InputRecord input)
        {
            var output = new List<OutputRecordSales>();

            using (NpgsqlConnection dbConnection = new NpgsqlConnection(
             _configuracoes.GetConnectionString("PostgresConnection")))
            {
                dbConnection.Open();

                string sqlQueryInsert = @"SELECT
                    t.""Id"" AS TransactionId,
                    t.""DateTransaction"",
                    t.""TotalSales"",
                    t.""ClientId"",
                    t.""PaymentId"",
                    t.""Parcel"",
                    pm.""Name"" AS PaymentMethodName,
                    c.""Name"" AS ClientName,
                    c.""Email"" AS ClientEmail,
                    c.""Phone"" AS ClientPhone,
                    c.""Address"" AS ClientAddress
                        FROM
                             transaction t
                        INNER JOIN
                            payment_methods pm ON t.""PaymentId"" = pm.""Id""
                        INNER JOIN
                            client c ON t.""ClientId"" = c.""Id""
                        WHERE
                            t.""DateTransaction"" >= @StartDate
                            AND t.""DateTransaction"" <= @EndDate
                            AND c.""UserId"" = @UserId";

                var recordInfo = await dbConnection.QueryAsync<SalesEntity>(sqlQueryInsert,
                    new
                    {
                        StartDate = input.StartDate,
                        EndDate = input.EndDate,
                        UserID = input.UserId
                    });

                if (recordInfo.Count() == 0)
                {
                    throw new ArgumentException("Erro ao encontrar dados de estoque");
                }
                else
                {
                    foreach (var record in recordInfo)
                    {
                        var stockInfo = new OutputRecordSales(record);
                        output.Add(stockInfo);
                    }

                    return output;
                }
            }
        }

            public async Task<ProductDto[]> GetStockReportInfo(InputRecord input)
            {
                using (NpgsqlConnection conexao = new NpgsqlConnection(
                    _configuracoes.GetConnectionString("PostgresConnection")))
                {
                    var products = await conexao.QueryAsync<Product>(
                        @"SELECT ""product"".* FROM ""product""
                        INNER JOIN ""user_"" ON ""product"".""UserID"" = ""user_"".""Id"" 
                        WHERE ""user_"".""Id"" = @UserId
                        AND ""product"".""CreationDate"" >= @StartDate
                        AND ""product"".""CreationDate"" <= @EndDate;",
                        new
                        {
                            UserId = input.UserId,
                            StartDate = input.StartDate,
                            EndDate = input.EndDate
                        });

                    if (products.Count() != 0)
                    {
                        var output = new List<ProductDto>();
                        foreach (var product in products)
                        {
                            var prod = new ProductDto(product);
                            output.Add(prod);
                        }

                        return output.ToArray();
                    }

                    else
                    {
                        throw new ArgumentException("Nenhum Produto Encontrado para esse Usuario");
                    }

                }
            }

        }
    }
