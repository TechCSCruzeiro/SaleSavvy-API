namespace SaleSavvy_API.Models.Client
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public Guid UserId { get; set; }

        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            this.ReturnCode = ReturnCode.failed;
            this.Error = new Error(message);
        }
    }
}
