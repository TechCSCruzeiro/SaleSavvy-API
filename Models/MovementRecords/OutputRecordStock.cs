namespace SaleSavvy_API.Models.MovementRecords
{
    public class OutputRecordStock
    {
        public OutputRecordStock() { }
        public OutputRecordStock(RecordEntity record) 
        { 
            this.Name = record.Name;
            this.Status = record.Status;
            this.DateMovement = record.DateMovement;
            this.CurrentQuantity = record.CurrentQuantity;
            this.MovementQuantity = record.MovementQuantity;
            this.NewValue = record.NewValue;
            this.OldValue = record.OldValue;
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
