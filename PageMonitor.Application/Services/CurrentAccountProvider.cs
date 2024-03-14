using PageMonitor.Application.Interfaces;
using PageMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PageMonitor.Application.Exceptions;

namespace PageMonitor.Application.Services
{
    public class CurrentAccountProvider : ICurrentAccountProvider
    {
        private readonly IAuthenticationDataProvider _authenticationDataProvider;
        private readonly IApplicationDbContext _applicationDbContext;

        public CurrentAccountProvider(IAuthenticationDataProvider authenticationDataProvider, IApplicationDbContext applicationDbContext)
        {
            _authenticationDataProvider = authenticationDataProvider;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int?> GetAccountId()
        {
            var userId = _authenticationDataProvider.GetUserId();
            if (userId != null)
            {
                return await _applicationDbContext.AccountUsers
                    .Where(au => au.UserId == userId)
                    .OrderBy(au => au.Id)
                    .Select(au => (int?)au.AccountId)
                    .FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<Account> GetAuthenticatedAccount()
        {
            var accountId = await GetAccountId();
            if ((accountId == null))
            {
                throw new UnauthorizedAccessException();
            }

            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId.Value);
            if (account == null)
            {
                throw new ErrorException("AccountDoesNotExist");
            }
            return account;
        }
    }
}