namespace SaleSavvy_API.Models.Register
{
    public class Register
    {

        public ReturnCode ReturnCode { get; set; }
        public string ErrorMessage { get; set; }

        public Register() { }
        public Register(ReturnCode returnCode) 
        {
            ReturnCode = returnCode;
        }

        public Register(ReturnCode returnCode, string errorMessage) 
        {
            ReturnCode = returnCode;
            ErrorMessage = errorMessage;
        }


    }
}
