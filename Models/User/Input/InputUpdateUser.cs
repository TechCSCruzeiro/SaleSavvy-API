namespace SaleSavvy_API.Models.User.Input
{
    public class InputUpdateUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
