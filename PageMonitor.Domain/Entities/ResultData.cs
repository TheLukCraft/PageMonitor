namespace PageMonitor.Domain.Entities
{
    public class ResultData
    {
        public required int StatusCode { get; set; }

        public required string Content { get; set; }

        public required TimeSpan ResponseTime { get; set; }
    }
}