using SaleSavvy_API.Models.MovementRecords.Entity;

namespace SaleSavvy_API.Models.MovementRecords.Output
{
    public class OutputRecordStock
    {
        public OutputRecordStock() { }
        public OutputRecordStock(RecordEntity record)
        {
            Name = record.Name;
            Status = record.Status;
            DateMovement = record.DateMovement;
            CurrentQuantity = record.CurrentQuantity;
            MovementQuantity = record.MovementQuantity;
            NewValue = record.NewValue;
            OldValue = record.OldValue;
        }

        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime DateMovement { get; set; }
        public int CurrentQuantity { get; set; }
        public int MovementQuantity { get; set; }
        public decimal? NewValue { get; set; }
        public decimal? OldValue { get; set; }
    }
}
