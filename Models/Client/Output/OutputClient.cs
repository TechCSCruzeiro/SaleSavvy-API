namespace SaleSavvy_API.Models.Client.Output
{
    public class OutputClient
    {
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            ReturnCode = ReturnCode.failed;
            Error = new Error(message);
        }
    }
}
