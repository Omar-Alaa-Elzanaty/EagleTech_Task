﻿using EagleTech_Task.Application.Features.Users.Commands.Create;
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

        [HttpGet()]
        public async Task<ActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}/subOrdinates")]
        public async Task<ActionResult>GetSubOrdinates(Guid id)
        {
            return Ok();
        }
    }
}