using EagleTech_Task.Application.Features.Products.Commands.Create;
using EagleTech_Task.Application.Features.Products.Queries.GetAllProducts;
using EagleTech_Task.Domain.Constant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EagleTech_Task.Presentation.Endpoints
{
    [Authorize(Roles = $"{Constants.Admin},{Constants.Supplier}")]
    public class ProductController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = Constants.Supplier)]
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
