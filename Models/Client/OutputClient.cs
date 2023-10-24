namespace SaleSavvy_API.Models.Client
{
    public class OutputClient
    {
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            this.ReturnCode = ReturnCode.failed;
            this.Error = new Error(message);
        }
    }
}
