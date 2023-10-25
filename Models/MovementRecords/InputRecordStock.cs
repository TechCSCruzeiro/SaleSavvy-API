namespace SaleSavvy_API.Models.MovementRecords
{
    public class InputRecordStock
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
    }
}
