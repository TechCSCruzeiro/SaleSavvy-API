namespace SaleSavvy_API.Models.Client.Entity
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}
