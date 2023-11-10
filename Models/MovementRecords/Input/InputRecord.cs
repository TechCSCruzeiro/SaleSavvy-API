namespace SaleSavvy_API.Models.MovementRecords.Input
{
    public class InputRecord
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
    }
}
