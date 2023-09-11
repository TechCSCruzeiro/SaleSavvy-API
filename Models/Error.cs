
namespace SaleSavvy_API.Models
{
    public class Error
    {
        public string[] MenssageError { get; set; }

        public Error() { }
        public Error(string[] menssageError)
        {
            MenssageError = menssageError;
        }
    }
}