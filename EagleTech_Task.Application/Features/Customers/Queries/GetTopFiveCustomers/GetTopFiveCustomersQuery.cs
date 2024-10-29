using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EagleTech_Task.Application.Features.Customers.Queries.GetTopFiveCustomers
{
    public class GetTopFiveCustomersQuery : IRequest<Result<List<GetTopFiveCustomersQueryDto>>>;

    internal class GetTopFiveCustomersQueryHandler : IRequestHandler<GetTopFiveCustomersQuery, Result<List<GetTopFiveCustomersQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopFiveCustomersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetTopFiveCustomersQueryDto>>> Handle(GetTopFiveCustomersQuery request, CancellationToken cancellationToken)
        {
            var top5Customer = await _unitOfWork.Repository<Customer>().Entities
                            .Where(x => x.Orders.Any(x => x.Status == OrderStatus.Delivered))
                            .OrderByDescending(x => x.Orders.Count())
                            .Take(5)
                            .ProjectToType<GetTopFiveCustomersQueryDto>()
                            .ToListAsync();

            return Result<List<GetTopFiveCustomersQueryDto>>.Success(top5Customer);
        }
    }
}
