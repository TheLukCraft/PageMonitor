using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PageMonitor.Application.Exceptions;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Logic.Abstractions;

namespace PageMonitor.Application.Logic.User;

public static class LoginCommand
{
    public class Request : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
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
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user != null)
            {
                if (_passwordManager.VerifyPassword(user.HashedPassword, request.Password))
                {
                    return new Result()
                    {
                        UserId = user.Id
                    };
                }
            }
            throw new ErrorException("InvalidLoginOrPassword");
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}