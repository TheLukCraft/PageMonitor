using PageMonitor.Domain.Entities;

namespace PageMonitor.Application.Interfaces
{
    public interface ICurrentAccountProvider
    {
        Task<Account> GetAuthenticatedAccount();

        Task<int> GetAccountId();
    }
}