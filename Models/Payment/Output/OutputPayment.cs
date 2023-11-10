namespace SaleSavvy_API.Models.Payment.Output
{
    public class OutputPayment
    {
        public Guid Id { get; set; }
        public string FormPayment { get; set; }
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            ReturnCode = ReturnCode.failed;
            Error = new Error(message);
        }

    }
}
