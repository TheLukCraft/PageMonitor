using PageMonitor.Domain.Common;

namespace PageMonitor.Domain.Entities
{
    public class Result : DomainEntity
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public int MonitoredUrlId { get; set; }

        public MonitoredUrl Url { get; set; } = default!;

        public bool Success { get; set; } = false;
    }
}