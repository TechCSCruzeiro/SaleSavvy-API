namespace SaleSavvy_API.Models.Login.Entity
{
    public class LoginEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdm { get; set; }
    }
}