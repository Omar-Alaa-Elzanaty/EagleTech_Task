using EagleTech_Task.Application.Features.Orders.Commands.Create;
using EagleTech_Task.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Presentation.Endpoints
{
    public class OrderController:ApiControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<GetOrdersByCustomerIdQueryDto>>>GetOrdersByCustomerId(Guid customerId)
        {
            return Ok(await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId)));
        }


    }
}
