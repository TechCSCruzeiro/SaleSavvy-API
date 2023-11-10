using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SaleSavvy_API.Models.Sales.Entity
{
    public class SalesEntity
    {
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
