using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Models.Products.Input;
using SaleSavvy_API.Models.Sales.Input;
using SaleSavvy_API.Models.Sales.Output;
using SaleSavvy_API.Models.Validates;

namespace SaleSavvy_API.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMovementRecordsRepository _movementRecordsRepository;
        public SalesService(
            ISalesRepository salesRepository,
            IPaymentRepository paymentRepository,
            IProductRepository productRepository,
            IMovementRecordsRepository movementRecordsRepository)
        {
            _salesRepository = salesRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _movementRecordsRepository = movementRecordsRepository;
        }

        public async Task<OutputSales> TransactionSales(InputSales input)
        {
            var output = new OutputSales();

            var validateSales = new ValidateSales();
            var valid = validateSales.Validate(input);

            if (valid.ReturnCode != ReturnCode.exito)
            {
                output.AddError(valid.Error.MenssageError);
                return output;
            }

            var validatePayment = new ValidatePayment();

            var payments = await _paymentRepository.FindAll();

            var validatePay = validatePayment.Validate(payments, input.Payment);

            if (validatePay.ReturnCode != ReturnCode.exito)
            {
                output.AddError(validatePay.Error.MenssageError);
                return output;
            }

            var sales = await _salesRepository.InsertTransaction(input, validatePay.Id);

            if (sales.ReturnCode != ReturnCode.exito)
            {
                output.AddError(sales.Error.MenssageError);
                return output;
            }


            foreach (var item in input.Product)
            {
                var product = await _productRepository.FindProductById(item.Id);

                if (product != null)
                {
                    if(item.Quantity - product.Quantity == 0)
                    {
                        product.IsActive = false;
                    }

                    if(product.Quantity - item.Quantity < 0)
                    {
                        var listError = new List<string>();
                        listError.Add("Quantidade de compra maior que quantidade de estoque");

                        output.AddError(listError.ToArray());
                        return output;
                    }

                    var resultProduct = await _productRepository.EditProduct(new Product(item, product, product.Quantity));;

                    if (resultProduct.ReturnCode != ReturnCode.exito)
                    {
                        output.AddError(sales.Error.MenssageError);
                        return output;
                    }

                    var itemSales = await _salesRepository.InsertItemsSales(sales.TransactionId, item);

                    if (itemSales.ReturnCode != ReturnCode.exito)
                    {
                        output.AddError(itemSales.Error.MenssageError);
                        return output;
                    }

                    var record = await _movementRecordsRepository.SaveRecord(StatusMovementRecords.Saida, new InputProduct(item, input.UserId));

                    if (record.ReturnCode != ReturnCode.exito)
                    {
                        output.AddError(record.Error.MenssageError);
                        return output;
                    }
                }
            }

            output.TransactionId = sales.TransactionId;
            output.ReturnCode = ReturnCode.exito;

            return output;
        }
    }
}
