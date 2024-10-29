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

namespace EagleTech_Task.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesQuery:IRequest<Result<List<GetAllRolesQueryDto>>>;

    internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<GetAllRolesQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRolesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllRolesQueryDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<Role>().GetAllAsync();


            return Result<List<GetAllRolesQueryDto>>.Success(roles.Adapt<List<GetAllRolesQueryDto>>());
        }
    }
}
