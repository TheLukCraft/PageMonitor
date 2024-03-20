using PageMonitor.Domain.Entities;
using PageMonitor.Domain.Enums;

namespace PageMonitor.Application.Services.ResultRuleCheckers.PropertyChecker
{
    public class ContentChecker : PropertyCheckerBase<string>
    {
        public ContentChecker() : base(ResultPropertyEnum.Content)
        {
        }

        protected override string ExtractValue(ResultData data)
        {
            return data.Content;
        }

        protected override bool IsSatisfied(ResultRule rule, string value)
        {
            var result = rule.Operator switch
            {
                ResultPropertyCompareOperatorEnum.Equal => string.Compare(value, rule.Value, StringComparison.InvariantCultureIgnoreCase) == 0,
                ResultPropertyCompareOperatorEnum.NotEqual => string.Compare(value, rule.Value, StringComparison.InvariantCultureIgnoreCase) != 0,
                ResultPropertyCompareOperatorEnum.Contains => value.Contains(rule.Value, StringComparison.CurrentCultureIgnoreCase),
                _ => throw new ArgumentException("Invalid operator")
            };

            return result;
        }
    }
}