using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;

namespace SaleSavvy_API.Services
{
    public class AutenticationService : IAutenticationService
    {
        IAutenticationRepository _autenticationRepository;
        public AutenticationService(IAutenticationRepository autenticationRepository) 
        {
            _autenticationRepository = autenticationRepository;
        }

        /// <summary>
        /// Validação de Login
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>n
        public async Task<OutputLogin> Validatelogin(InputLogin input)
        {
            var minimumPasswordLength = 8;
            var maximumPasswordLength = 20;

            if (input.Password.Length < minimumPasswordLength || input.Password.Length > maximumPasswordLength)
            {
                throw new ArgumentException("Quantidade de Caracteres invalido", "Min:" + minimumPasswordLength + "Max:" + maximumPasswordLength);
            }

            if (string.IsNullOrEmpty(input.Login))
            {
                throw new ArgumentException("Campo de Login Vazio");
            }

            return await _autenticationRepository.GetLogin(input, Guid.NewGuid());
        }
    }
}
