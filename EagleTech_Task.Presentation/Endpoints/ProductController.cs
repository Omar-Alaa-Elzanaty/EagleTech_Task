using EagleTech_Task.Application.Features.Products.Commands.Create;
using EagleTech_Task.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Presentation.Endpoints
{
    public class ProductController:ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllProductsQueryDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }
    }
}
