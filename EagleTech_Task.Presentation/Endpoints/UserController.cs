using EagleTech_Task.Application.Features.Roles.Queries.GetAll;
using EagleTech_Task.Application.Features.Users.Commands.Create;
using EagleTech_Task.Application.Features.Users.Queries.GetAll;
using EagleTech_Task.Application.Features.Users.Queries.GetManagerUser;
using EagleTech_Task.Application.Features.Users.Queries.GetSubOrdinates;
using EagleTech_Task.Domain.Constant;
using EagleTeck_Task.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EagleTech_Task.Presentation.Endpoints
{
    [Authorize(Roles = Constants.Admin)]
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createAccount")]
        public async Task<ActionResult<Result<string>>> Register(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<GetAllUsersQueryDto>>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("{id}/subOrdinates")]
        public async Task<ActionResult<Result<List<GetSubOrdinatesQueryDto>>>>GetSubOrdinates(Guid id)
        {
            return Ok(await _mediator.Send(new GetSubOrdinatesQuery(id)));
        }

        [HttpGet("managerUsers")]
        public async Task<ActionResult<Result<List<GetManagerUserQueryDto>>>> GetManagerUsers()
        {
            return Ok(await _mediator.Send(new GetManagerUserQuery()));
        }

        [HttpGet("roles")]
        public async Task<ActionResult<Result<List<GetAllRolesQueryDto>>>> GetRoles()
        {
            return Ok(await _mediator.Send(new GetAllRolesQuery()));
        }
    }
}