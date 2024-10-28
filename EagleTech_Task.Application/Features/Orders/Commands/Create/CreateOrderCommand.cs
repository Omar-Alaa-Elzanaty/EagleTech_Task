using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Orders.Commands.Create
{
    public record CreateOrderCommand:IRequest<Result<Guid>>
    {
        public Guid CustomerId { get; set; }
        public List<ProductItemDto> Items { get; set; }
    }

    public record ProductItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
                var products = await _unitOfWork.Repository<Product>().Entities
                        .Where(x => command.Items.Select(i => i.ProductId).ToList().Contains(x.Id))
                        .ToListAsync();

                if(products.Count != command.Items.Count)
                {
                    return Result<Guid>.Failure("Some products not exist.");
                }

            var order = command.Adapt<Order>();

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveAsync(cancellationToken);

            order.Details.AddRange(command.Items.Adapt<List<OrderDetail>>());

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result<Guid>.Success(order.Id);
        }
    }
}
