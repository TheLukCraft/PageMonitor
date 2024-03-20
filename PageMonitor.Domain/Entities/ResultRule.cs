using PageMonitor.Domain.Enums;

namespace PageMonitor.Domain.Entities
{
    public class ResultRule
    {
        public required ResultPropertyEnum Property { get; set; }

        public required string Value { get; set; }

        public required ResultPropertyCompareOperatorEnum Operator { get; set; }
    }
}