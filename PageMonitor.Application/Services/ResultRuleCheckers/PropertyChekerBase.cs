using PageMonitor.Domain.Entities;
using PageMonitor.Domain.Enums;

namespace PageMonitor.Application.Services.ResultRuleCheckers
{
    public abstract class PropertyCheckerBase<T> : IPropertyChecker
    {
        protected readonly ResultPropertyEnum _property;

        public PropertyCheckerBase(ResultPropertyEnum property)
        {
            _property = property;
        }

        public virtual bool CanHandle(ResultRule rule)
        {
            return rule.Property == _property;
        }

        public bool Check(ResultRule rule, ResultData data)
        {
            if (rule.Property != _property)
            {
                throw new ArgumentException("Invalid rule property");
            }

            var value = ExtractValue(data);
            return IsSatisfied(rule, value);
        }

        protected abstract T ExtractValue(ResultData data);

        protected abstract bool IsSatisfied(ResultRule rule, T value);
    }
}