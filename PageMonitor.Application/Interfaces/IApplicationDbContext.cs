using Microsoft.EntityFrameworkCore;
using PageMonitor.Domain.Entities;

namespace PageMonitor.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountUser> AccountUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}