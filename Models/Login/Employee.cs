using SaleSavvy_API.Models.Login.Entity;

namespace SaleSavvy_API.Models.Login
{
    public class Employee
    {
        public Employee() { }
        public Employee(string email, string password, string name, bool isAdm)
        {
            this.Email = email;
            this.Password = password;
            this.Name = name;
            this.IsAdm = isAdm;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdm { get; set; }

        public Employee AddEmployee(LoginEntity entity)
        {
            var employee = new Employee();

            employee.Email = entity.Email;
            employee.Password = entity.Password;
            employee.Name = entity.Name;
            employee.IsAdm = entity.IsAdm;

            return employee;

        }
    }
}