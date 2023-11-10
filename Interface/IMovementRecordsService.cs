using SaleSavvy_API.Models.MovementRecords.Input;

namespace SaleSavvy_API.Interface
{
    public interface IMovementRecordsService
    {
        public Task<Guid> CreateMovementRecordStock(InputRecord input);
        public Task<Guid> CreateRecordStock(InputRecord input);
        public Task<Guid> CreateSallesRecord(InputRecord input);
        public Task<string> SearchRecord(Guid fileId);
    }
}
