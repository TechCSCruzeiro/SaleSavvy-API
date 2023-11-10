namespace SaleSavvy_API.Models.Products.Output
{
    public class OutputProduct
    {
        public Product Product { get; set; }
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            ReturnCode = ReturnCode.failed;
            Error = new Error(message);
        }
    }
}
