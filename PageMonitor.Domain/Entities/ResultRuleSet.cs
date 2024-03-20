using PageMonitor.Domain.Enums;

namespace PageMonitor.Domain.Entities
{
    public class ResultRuleSet
    {
        public List<ResultRule> Rules { get; set; } = new List<ResultRule>();

        public ResultRuleSetOperatorEnum Operator { get; set; } = ResultRuleSetOperatorEnum.Or;
    }
}