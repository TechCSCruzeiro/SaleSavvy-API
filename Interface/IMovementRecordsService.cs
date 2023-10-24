using SaleSavvy_API.Models.MovementRecords;

namespace SaleSavvy_API.Interface
{
    public interface IMovementRecordsService
    {
        public Task<Guid> CreateRecordStock(InputRecordStock input);
        public Task<string> SearchRecord(Guid fileId);
    }
}
