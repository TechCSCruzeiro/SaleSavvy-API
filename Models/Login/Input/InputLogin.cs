namespace SaleSavvy_API.Models.Login.Input
{
    public class InputLogin
    {

        public Guid ID => Guid.NewGuid();

        /// <summary>
        /// email de acesso ao controlador de estoque
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// senha de acesso ao controlador de estoque
        /// </summary>
        public string Password { get; set; }

    }
}