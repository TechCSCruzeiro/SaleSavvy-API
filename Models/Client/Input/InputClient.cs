namespace SaleSavvy_API.Models.Client.Input
{
    public class InputClient
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid UserID { get; set; }
        public Address Address { get; set; }
    }
}

