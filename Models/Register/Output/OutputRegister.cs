namespace SaleSavvy_API.Models.Register.Output
{
    public class OutputRegister
    {
        public Error Error { get; set; }
        public ReturnCode ReturnCode { get; set; }

        public OutputRegister() { }

        public void AddError(ReturnCode returnCode, string[] message)
        {
            this.ReturnCode = returnCode;
            this.Error = new Error(message);
        }
    }
}
