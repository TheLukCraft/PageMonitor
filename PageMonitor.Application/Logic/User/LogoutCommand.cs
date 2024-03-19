using FluentValidation;
using MediatR;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Logic.Abstractions;

namespace PageMonitor.Application.Logic.User;

public static class LogoutCommand
{
    public class Request : IRequest<Result>
    {
    }

    public class Result
    {
    }

    public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
    {
        public Handler(ICurrentAccountProvider currentAccountProvider, IApplicationDbContext applicationDbContext) : base(currentAccountProvider, applicationDbContext)
        {
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            return new Result()
            {
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