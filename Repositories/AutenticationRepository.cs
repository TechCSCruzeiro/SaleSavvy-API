using Dapper;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Entity;
using SaleSavvy_API.Models.Login.Input;
using System.Data;
using Microsoft.Data.SqlClient;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register;
using Npgsql;

namespace SaleSavvy_API.Repositories
{

    public class AutenticationRepository : IAutenticationRepository
    {

        private IConfiguration _configuracoes;

        public AutenticationRepository(IConfiguration config)
        {
            _configuracoes = config;
        }

        public async Task<Login> GetLogin(InputLogin input)
        {
            using (NpgsqlConnection conexao = new NpgsqlConnection(
                _configuracoes.GetConnectionString("PostgresConnection")))
            {

                // Consulta SQL parametrizada
                string consulta = "SELECT * FROM \"user_\" WHERE \"Email\" = @Email";

                // Executar a consulta e passar o parâmetro
                var resultados = await conexao.QueryAsync<LoginEntity>(consulta, new { Email = input.Email });

                // Verifique se há resultados
                if (resultados.Any())
                {
                    // Transforme o resultado em uma instância de LoginEntity
                    var loginEntity = resultados.First();

                    // Crie uma instância de Login e preencha-a com os valores do LoginEntity
                    var login = new Login(loginEntity);

                    return login;
                }
                else
                {
                    // Se não houver resultados, retorne null ou trate de outra forma
                    return null;
                }
            }
        }
    }
}
