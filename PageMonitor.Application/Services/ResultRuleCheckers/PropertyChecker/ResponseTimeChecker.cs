using PageMonitor.Domain.Entities;
using PageMonitor.Domain.Enums;

namespace PageMonitor.Application.Services.ResultRuleCheckers.PropertyChecker
{
    public class ResponseTimeChecker : PropertyCheckerBase<int>
    {
        public ResponseTimeChecker() : base(ResultPropertyEnum.ResponseTime)
        {
        }

        protected override int ExtractValue(ResultData data)
        {
            return (int)Math.Round(data.ResponseTime.TotalMilliseconds);
        }

        protected override bool IsSatisfied(ResultRule rule, int value)
        {
            if (!int.TryParse(rule.Value, out int ruleResponseTime))
            {
                throw new ArgumentException("Invalid response time", nameof(rule.Value));
            }

            var result = rule.Operator switch
            {
                ResultPropertyCompareOperatorEnum.GreaterThan => value > ruleResponseTime,
                ResultPropertyCompareOperatorEnum.LessThan => value < ruleResponseTime,
                _ => throw new ArgumentException("Invalid operator")
            };

            return result;
        }
    }
}