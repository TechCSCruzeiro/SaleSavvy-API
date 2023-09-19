namespace SaleSavvy_API.Models.Products
{
    public class OutputProduct
    {
        public Product Product { get; set; }
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError( string[] message)
        {
            this.ReturnCode = ReturnCode.failed;
            this.Error = new Error(message);
        }
    }
}
