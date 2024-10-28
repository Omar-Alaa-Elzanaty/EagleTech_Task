using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EagleTech_Task.Application.Features.Orders.Queries.GetOrdersTotal
{
    public record GetOrdersTotalQuery:IRequest<Result<List<GetOrdersTotalQueryDto>>>;

    internal class GetOrdersTotalQueryHandler : IRequestHandler<GetOrdersTotalQuery, Result<List<GetOrdersTotalQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersTotalQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetOrdersTotalQueryDto>>> Handle(GetOrdersTotalQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().Entities
                       .Select(x => new GetOrdersTotalQueryDto()
                       {
                           Id = x.Id,
                           TotalAmount = x.Details.Sum(x => x.Quantity * x.Product.Price)
                       }).ToListAsync();

            return Result<List<GetOrdersTotalQueryDto>>.Success(orders);
        }
    }
}
