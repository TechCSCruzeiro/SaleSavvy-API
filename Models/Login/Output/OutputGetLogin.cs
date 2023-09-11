using static Dapper.SqlMapper;

namespace SaleSavvy_API.Models.Login.Input
{
    public class OutputGetLogin
    {
        public OutputGetLogin()
        {

        }

        public OutputGetLogin(Login login)
        {
            var input = login.EmployeeLogin;

            this.Id = login.Id;
            this.EmployeeLogin = new EmployeeLogin(input.Email, new string('*', input.Password.Length), input.Name);
            this.ReturnCode = login.ReturnCode;

        }

        public Guid Id { get; set; }
        public EmployeeLogin EmployeeLogin { get; set; }
        public Error Error { get; set; }
        public ReturnCode ReturnCode { get; set; }

        public void AddError(ReturnCode returnCode, string[] message)
        {
            this.ReturnCode = returnCode;
            this.Error = new Error(message);
        }


    }


}