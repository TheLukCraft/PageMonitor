using PageMonitor.Domain.Common;

namespace PageMonitor.Domain.Entities
{
    public class Account : DomainEntity
    {
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();
    }
}