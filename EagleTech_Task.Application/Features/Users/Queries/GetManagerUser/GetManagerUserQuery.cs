using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Users.Queries.GetManagerUser
{
    public record GetManagerUserQuery:IRequest<Result<List<GetManagerUserQueryDto>>>;

    internal class GetManagerUserQueryHandler : IRequestHandler<GetManagerUserQuery, Result<List<GetManagerUserQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetManagerUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetManagerUserQueryDto>>> Handle(GetManagerUserQuery request, CancellationToken cancellationToken)
        {
            var managerUser = await _unitOfWork.Repository<User>().Entities
                            .Where(x => x.Roles.Any(x => x.Name == Constants.TechnicalSupport || x.Name == Constants.Supplier))
                            .ProjectToType<GetManagerUserQueryDto>()
                            .ToListAsync();

            return Result<List<GetManagerUserQueryDto>>.Success(managerUser);
        }
    }
}
