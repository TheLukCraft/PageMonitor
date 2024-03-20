using PageMonitor.Domain.Common;

namespace PageMonitor.Domain.Entities
{
    public class MonitoredUrl : DomainEntity
    {
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        public int AccountId { get; set; }

        public Account Account { get; set; } = default!;

        public string Url { get; set; } = "";

        public ResultRuleSet RuleSet { get; set; } = new ResultRuleSet();

        public List<Result> Results { get; set; } = new List<Result>();
    }
}