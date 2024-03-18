using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageMonitor.Application.Logic.User;

namespace PageMonitor.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(ILogger<UserController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserWithAccount([FromBody] CreateUserWithAccountCommand.Request model)
        {
            var createAccountResult = await _mediator.Send(model);
            return Ok(createAccountResult);
        }
    }
}