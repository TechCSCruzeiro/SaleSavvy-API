using SaleSavvy_API.Models;
using System;
using System.Security.Cryptography.X509Certificates;

public class OutputLogin
{
    static string mensageExito = "Cadastro Efetuado com Sucesso";
    static string mensageFailed = "Erro ao Cadastrar";

    /// <summary>
    /// 
    /// </summary>
    public ReturnCode ReturnCode { get; set; }
    public string ReturnMensage => ReturnCode == ReturnCode.exito ? mensageExito : mensageFailed;
    public bool StatusCadastration => ReturnCode == ReturnCode.exito ? true : false;

    public OutputLogin()
    {

    }
}
