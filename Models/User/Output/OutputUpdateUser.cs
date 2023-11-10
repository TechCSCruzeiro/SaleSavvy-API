namespace SaleSavvy_API.Models.User.Output
{
    public class OutputUpdateUser
    {
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public OutputUpdateUser() { }
        public OutputUpdateUser(ReturnCode returnCode)
        {
            ReturnCode = returnCode;
        }


        public void AddError(ReturnCode returnCode, string[] message)
        {
            ReturnCode = returnCode;
            Error = new Error(message);
        }
    }
}
