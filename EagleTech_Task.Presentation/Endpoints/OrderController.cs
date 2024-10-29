using EagleTech_Task.Application.Features.Orders.Commands.Create;
using EagleTech_Task.Application.Features.Orders.Commands.UpdateStatus;
using EagleTech_Task.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using EagleTech_Task.Application.Features.Orders.Queries.GetOrdersTotal;
using EagleTech_Task.Domain.Constant;
using EagleTeck_Task.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EagleTech_Task.Presentation.Endpoints
{
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = Constants.Admin)]
        public async Task<ActionResult<Result<Guid>>> Create(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{customerId}")]
        [Authorize(Roles = $"{Constants.TechnicalSupport},{Constants.Admin}")]
        public async Task<ActionResult<List<GetOrdersByCustomerIdQueryDto>>> GetOrdersByCustomerId(Guid customerId)
        {
            return Ok(await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId)));
        }

        [HttpGet("getTotalOrders")]
        public async Task<ActionResult<GetOrdersTotalQueryDto>> GetTotalOrders()
        {
            return Ok(await _mediator.Send(new GetOrdersTotalQuery()));
        }

        [HttpPut]
        [Authorize(Roles = Constants.Supplier)]
        public async Task<ActionResult<Result<string>>> UpdateStatus(UpdateOrderStatusCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
