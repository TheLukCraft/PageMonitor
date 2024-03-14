using PageMonitor.Domain.Common;

namespace PageMonitor.Domain.Entities
{
    public class AccountUser : DomainEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; } = default!;
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}