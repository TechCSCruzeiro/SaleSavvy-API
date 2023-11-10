using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.Payment.Output;

namespace SaleSavvy_API.Models.Validates
{
    public class ValidatePayment
    {
        public OutputPayment Validate(List<OutputPayment> listPayment, string formPaymennt)
        {
            var output = new OutputPayment();
            var listError = new List<string>();

            output.ReturnCode = ReturnCode.failed;

            foreach (var item in listPayment)
            {
                if (formPaymennt.ToUpper() == item.FormPayment.ToUpper())
                {
                    output.ReturnCode = ReturnCode.exito;
                    output.FormPayment = item.FormPayment;
                    output.Id = item.Id;

                    return output;
                }
            }

            listError.Add("Forma de pagamento não encontrada na base");
            output.AddError(listError.ToArray());

            return output;
        }
    }
}
