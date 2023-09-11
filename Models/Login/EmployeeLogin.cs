using SaleSavvy_API.Models.Login.Entity;

namespace SaleSavvy_API.Models.Login
{
    public class EmployeeLogin
    {
        public EmployeeLogin() { }
        public EmployeeLogin(string email, string password, string name)
        {
            this.Email = email;
            this.Password = password;
            this.Name = name;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public EmployeeLogin AddEmployee(LoginEntity entity)
        {
            var employee = new EmployeeLogin();

            employee.Email = entity.Email;
            employee.Password = entity.Password;
            employee.Name = entity.Name;

            return employee;

        }
    }
}