using PageMonitor.Domain.Entities;

namespace PageMonitor.Application.Services.ResultRuleCheckers
{
    public interface IPropertyChecker
    {
        bool CanHandle(ResultRule rule);

        bool Check(ResultRule rule, ResultData data);
    }
}