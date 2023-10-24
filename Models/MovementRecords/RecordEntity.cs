namespace SaleSavvy_API.Models.MovementRecords
{
    public class RecordEntity
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime DateMovement { get; set; }
        public int CurrentQuantity { get; set; }
        public int MovementQuantity { get; set; }
        public decimal? NewValue { get; set; }
        public decimal? OldValue { get; set; }
    }
}
