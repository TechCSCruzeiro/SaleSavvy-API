namespace SaleSavvy_API.Models.MovementRecords
{
    public class OutputRecord
    {
        public int UrlDownload { get; set; }

        public ReturnCode ReturnCode { get; set; }
        public Error Error { get; set; }

        public void AddError(string[] message)
        {
            this.ReturnCode = ReturnCode.failed;
            this.Error = new Error(message);
        }
    }
}
