using SaleSavvy_API.Models.Login.Entity;

namespace SaleSavvy_API.Models.Login
{
    public class Login
    {
        public Login()
        {
        }

        public Login(LoginEntity output)
        {
            Id = output.Id;
            EmployeeLogin = new Employee(output.Email, output.Password, output.Name);
            this.ReturnCode = ReturnCode.exito;
        }

        public Login(ReturnCode returnCode, string[] error)
        {
            this.Error = new Error(error);
            this.ReturnCode = returnCode;
        }

        public Guid Id { get; set; }
        public Employee EmployeeLogin { get; set; }
        public Error Error { get; set; }
        public ReturnCode ReturnCode { get; set; }

        public Login AddError(string message)
        {
            var errorList = new List<string>();
            errorList.Add(message);
            var error = errorList.ToArray();

            return new Login(ReturnCode.failed, error);
        }
    }
}