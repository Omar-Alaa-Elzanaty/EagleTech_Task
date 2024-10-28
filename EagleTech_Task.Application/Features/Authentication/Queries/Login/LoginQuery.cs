using EagleTech_Task.Application.Interfaces.Auth;
using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Authentication.Queries.Login
{
    public  record LoginQuery:IRequest<Result<LoginQueryDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    internal class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthServices _authServices;

        public LoginQueryHandler(
            IUnitOfWork unitOfWork,
            IAuthServices authServices)
        {
            _unitOfWork = unitOfWork;
            _authServices = authServices;
        }

        public async Task<Result<LoginQueryDto>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().Entities
                    .FirstOrDefaultAsync(x => x.UserName == query.UserName);

            if (user == null)
            {
                return Result<LoginQueryDto>.Failure("Unauthorized.", HttpStatusCode.Unauthorized);
            }

            if (!_authServices.VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Result<LoginQueryDto>.Failure("Unauthorized.", HttpStatusCode.Unauthorized);
            }

            var response = user.Adapt<LoginQueryDto>();

            response.Token = _authServices.GenerateToken(new(user.Id, user.UserName, user.Email, user.Roles.Select(x=>x.Name).ToList()));

            return Result<LoginQueryDto>.Success(response);
        }
    }
}
