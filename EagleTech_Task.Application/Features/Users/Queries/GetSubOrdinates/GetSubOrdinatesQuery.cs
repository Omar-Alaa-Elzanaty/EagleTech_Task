using EagleTech_Task.Application.Extensions;
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

namespace EagleTech_Task.Application.Features.Users.Queries.GetSubOrdinates
{
    public record GetSubOrdinatesQuery:IRequest<Result<List<GetSubOrdinatesQueryDto>>>
    {
        public Guid UserId { get; set; }

        public GetSubOrdinatesQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    internal class GetSubOrdinatesQueryHandler : IRequestHandler<GetSubOrdinatesQuery, Result<List<GetSubOrdinatesQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubOrdinatesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetSubOrdinatesQueryDto>>> Handle(GetSubOrdinatesQuery query, CancellationToken cancellationToken)
        {
            var subOrdinates = await _unitOfWork.Repository<User>().Entities
                            .Where(x => x.ManagerId == query.UserId)
                            .ProjectToType<GetSubOrdinatesQueryDto>()
                            .ToListAsync(cancellationToken);

            return Result<List<GetSubOrdinatesQueryDto>>.Success(subOrdinates);
        }
    }
}
