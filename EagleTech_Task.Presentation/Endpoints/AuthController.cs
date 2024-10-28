using EagleTech_Task.Application.Features.Authentication.Queries.Login;
using EagleTeck_Task.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EagleTech_Task.Presentation.Endpoints
{
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result<LoginQueryDto>>> Login(LoginQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
