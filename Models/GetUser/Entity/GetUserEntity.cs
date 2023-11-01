namespace SaleSavvy_API.Models.GetUser.Entity
{
    public class GetUserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdm { get; set; }
    }
}
