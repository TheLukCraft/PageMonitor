using EFCoreSecondLevelCacheInterceptor;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PageMonitor.Application.Exceptions;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Logic.Abstractions;
using System.CodeDom;

namespace PageMonitor.Application.Logic.User;

public static class LoggedInUserCommand
{
    public class Request : IRequest<Result>
    {
    }

    public class Result
    {
        public required string Email { get; set; }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        private readonly IAuthenticationDataProvider _authenticationDataProvider;

        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext, IAuthenticationDataProvider authenticationDataProvider) : base(currentAccountProvider, applicationDbContext)
        {
            _authenticationDataProvider = authenticationDataProvider;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var userId = _authenticationDataProvider.GetUserId();

            if (userId.HasValue)
            {
                var user = await _applicationDbContext.Users.Cacheable().FirstOrDefaultAsync(u => u.Id == userId.Value);
                if (user != null)
                {
                    return new Result()
                    {
                        Email = user.Email,
                    };
                }
            }
            throw new UnauthorizedException();
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }
}