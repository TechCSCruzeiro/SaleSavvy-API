using Microsoft.IdentityModel.Tokens;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.Login.Output;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SaleSavvy_API.Services
{
    //gerar Token
    public class TokenService
    {
        public static object GenerateToken(OutputGetLogin login)
        {
            //Chamar chave privada criada
            var key = Encoding.ASCII.GetBytes(Key.Secret);

            //configurar token
            var tokenConfig = new SecurityTokenDescriptor()
            {
                //salvar o id dentro do token
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("employeeId", login.Id.ToString()),
                    new Claim("name", login.EmployeeLogin.Name),
                }),
                //Tempo de expiração
                Expires = DateTime.UtcNow.AddHours(4),

                //tipo de assinatura/ tipo de criptografia
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //gerar token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Criar token
            var token = tokenHandler.CreateToken(tokenConfig);

            //hash do token
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
