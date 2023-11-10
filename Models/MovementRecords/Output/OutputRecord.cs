namespace SaleSavvy_API.Models.MovementRecords.Output
{
    public class OutputRecord
    {
        public int UrlDownload { get; set; }

        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            ReturnCode = ReturnCode.failed;
            Error = new Error(message);
        }
    }
}
