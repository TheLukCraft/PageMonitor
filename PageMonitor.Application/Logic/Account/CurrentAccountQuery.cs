using EFCoreSecondLevelCacheInterceptor;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PageMonitor.Application.Exceptions;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Logic.Abstractions;

namespace PageMonitor.Application.Logic.Account;

public static class CurrentAccountQuery
{
    public class Request : IRequest<Result>
    {
    }

    public class Result
    {
        public required string Name { get; set; }
    }

    public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await _currentAccountProvider.GetAuthenticatedAccount();

            return new Result()
            {
                Name = account.Name,
            };
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }
}