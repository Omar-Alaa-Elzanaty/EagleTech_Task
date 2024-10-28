using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EagleTech_Task.Application.Features.Users.Queries.GetAll
{
    public record GetAllUsersQuery : IRequest<Result<List<GetAllUsersQueryDto>>>;

    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllUsersQueryDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Repository<User>().Entities
                        .ProjectToType<GetAllUsersQueryDto>()
                        .ToListAsync();

            return Result<List<GetAllUsersQueryDto>>.Success(users);
        }
    }
}
