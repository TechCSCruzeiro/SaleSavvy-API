﻿using SaleSavvy_API.Models.Login.Input;

namespace SaleSavvy_API.Models.Login
{
    public class Validate
    {
        public OutputGetLogin ValidateLogin(InputLogin input, Login getLogin)
        {
            var output = new OutputGetLogin(getLogin);

            if (input.Password != getLogin.EmployeeLogin.Password)
            {

                var listError = new List<string>();

                listError.Add("Senha Invalida - TENTE NOVAMENTE");

                output.AddError(ReturnCode.failed, listError.ToArray());
                output.EmployeeLogin.Password = "";
                output.EmployeeLogin.Name = "";

                return output;
            }

            return output;

        }

    }
}