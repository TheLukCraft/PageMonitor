using PageMonitor.Domain.Common;

namespace PageMonitor.Domain.Entities
{
    public class User : DomainEntity
    {
        public string Email { get; set; } = default!;
        public string HashedPassword { get; set; } = default!;
        public DateTimeOffset RegisterDate { get; set; }
        public ICollection<AccountUser> Users { get; set; } = new List<AccountUser>();
    }
}