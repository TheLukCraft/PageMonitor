using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageMonitor.Application.Logic.Account;

namespace PageMonitor.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(ILogger<AccountController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrentAccount()
        {
            var data = await _mediator.Send(new CurrentAccountQuery.Request() { });
            return Ok(data);
        }
    }
}