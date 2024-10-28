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

namespace EagleTech_Task.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public record GetOrdersByCustomerIdQuery:IRequest<Result<List<GetOrdersByCustomerIdQueryDto>>>
    {
        public Guid CustomerId { get; set; }

        public GetOrdersByCustomerIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }

    internal class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, Result<List<GetOrdersByCustomerIdQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetOrdersByCustomerIdQueryDto>>> Handle(GetOrdersByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().Entities
                        .Where(x => x.CustomerId == query.CustomerId)
                        .ProjectToType<GetOrdersByCustomerIdQueryDto>()
                        .ToListAsync();

            return Result<List<GetOrdersByCustomerIdQueryDto>>.Success(orders);
        }
    }
}
