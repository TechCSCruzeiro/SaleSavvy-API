namespace SaleSavvy_API.Models.UpdateUser
{
    public class OutputUpdateUser
    {
        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public OutputUpdateUser() { }
        public OutputUpdateUser(ReturnCode returnCode) 
        { 
            this.ReturnCode = returnCode;
        }


        public void AddError(ReturnCode returnCode, string[] message)
        {
            this.ReturnCode = returnCode;
            this.Error = new Error(message);
        }
    }
}
