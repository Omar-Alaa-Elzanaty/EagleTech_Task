using EagleTech_Task.Application.Features.Customers.Commands.Create;
using EagleTech_Task.Application.Features.Customers.Queries.GetAllCustomers;
using EagleTech_Task.Application.Features.Customers.Queries.GetTopFiveCustomers;
using EagleTech_Task.Domain.Constant;
using EagleTeck_Task.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EagleTech_Task.Presentation.Endpoints
{
    [Authorize(Roles = Constants.Admin)]
    public class CustomerController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateCustomerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResult<GetAllCustomersQueryDto>>> GetAll(GetAllCustomersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("topFiveCustomers")]
        public async Task<ActionResult<Result<GetTopFiveCustomersQueryDto>>> GetTopFiveCustomers()
        {
            return Ok(await _mediator.Send(new GetTopFiveCustomersQuery()));
        }
    }
}
