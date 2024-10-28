using EagleTech_Task.Application.Extensions;
using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Customers.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery : IRequest<PaginationResult<GetAllCustomersQueryDto>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    internal class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginationResult<GetAllCustomersQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCustomerQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<GetAllCustomersQueryDto>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.Repository<Customer>().Entities
                          .ProjectToType<GetAllCustomersQueryDto>()
                          .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return customers;
        }
    }
}
