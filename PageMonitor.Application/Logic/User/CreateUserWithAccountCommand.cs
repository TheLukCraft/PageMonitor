using MediatR;
using PageMonitor.Application.Exceptions;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Logic.Abstractions;
using PageMonitor.Domain.Entities;
using System.Data.Entity;

namespace PageMonitor.Application.Logic.User
{
    public static class CreateUserWithAccountCommand
    {
        public class Request : IRequest<Result>
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class Result
        {
            public int UserId { get; set; }
        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            private readonly IPasswordManager _passwordManager;

            public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext, IPasswordManager passwordManager) : base(currentAccountProvider, applicationDbContext)
            {
                _passwordManager = passwordManager;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var userExists = await _applicationDbContext.Users.AnyAsync(u => u.Email == request.Email);

                if (userExists)
                {
                    throw new ErrorException("AccountWithThisEmailAlreadyExists");
                }

                var utcNow = DateTime.UtcNow;
                var user = new Domain.Entities.User()
                {
                    RegisterDate = utcNow,
                    Email = request.Email,
                    HashedPassword = "",
                };

                user.HashedPassword = _passwordManager.HashPassword(request.Password);

                _applicationDbContext.Users.Add(user);

                var account = new Domain.Entities.Account()
                {
                    Name = request.Email,
                    CreateDate = utcNow,
                };

                _applicationDbContext.Accounts.Add(account);

                var accountUser = new AccountUser()
                {
                    Account = account,
                    User = user,
                };

                _applicationDbContext.AccountUsers.Add(accountUser);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return new Result()
                {
                    UserId = user.Id,
                };
            }
        }
    }
}