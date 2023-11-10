using SaleSavvy_API.Models.Sales.Entity;

namespace SaleSavvy_API.Models.MovementRecords.Output
{
    public class OutputRecordSales
    {
        public OutputRecordSales() { }
        public OutputRecordSales(SalesEntity entity)
        {
            TransactionId = entity.TransactionId;
            DateTransaction = entity.DateTransaction;
            TotalSales = entity.TotalSales;
            ClientId = entity.ClientId;
            PaymentId = entity.PaymentId;
            Parcel = entity.Parcel;
            PaymentMethodName = entity.PaymentMethodName;
            ClientName = entity.ClientName;
            ClientEmail = entity.ClientEmail;
            ClientPhone = entity.ClientPhone;
            ClientAddress = entity.ClientAddress;
        }

        public Guid TransactionId { get; set; }
        public DateTime DateTransaction { get; set; }
        public decimal TotalSales { get; set; }
        public Guid ClientId { get; set; }
        public Guid PaymentId { get; set; }
        public int Parcel { get; set; }
        public string PaymentMethodName { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
    }
}
