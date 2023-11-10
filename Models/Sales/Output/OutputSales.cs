namespace SaleSavvy_API.Models.Sales.Output
{
    public class OutputSales
    {
        public Guid TransactionId { get; set; }
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            ReturnCode = ReturnCode.failed;
            Error = new Error(message);
        }
    }
}
