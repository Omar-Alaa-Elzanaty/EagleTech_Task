using EagleTech_Task.Application.Interfaces.Auth;
using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EagleTech_Task.Application.Features.Users.Commands.Create
{
    public record CreateUserCommand : IRequest<Result<Guid>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Guid? ManagerId { get; set; }
    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthServices _authServices;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateUserCommand> validator,
            IAuthServices authServices)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _authServices = authServices;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result<Guid>.ValidationFailure(validationResult.Errors, HttpStatusCode.UnprocessableEntity);
            }

            var role = await _unitOfWork.Repository<Role>().Entities.SingleOrDefaultAsync(x => x.Id == command.RoleId);

            if (role == null)
            {
                return Result<Guid>.Failure("Role not found.");
            }

            var userCheck = await CheckUserExist(command);

            if (!userCheck.IsSuccess)
            {
                return userCheck;
            }

            var user = command.Adapt<User>();

            _authServices.CreatePasswordHash(command.Password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveAsync(cancellationToken);

            user.Roles = [];
            user.Roles.Add(role);
            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);
        }

        private async Task<Result<Guid>> CheckUserExist(CreateUserCommand command)
        {
            var entity = _unitOfWork.Repository<User>().Entities;

            if (await entity.AnyAsync(x => x.UserName == command.UserName))
            {
                return ReturnReasonFail("Username already exist.");
            }

            if (await entity.AnyAsync(x => x.Email == command.Email))
            {
                return ReturnReasonFail("Email already exist.");
            }

            return Result<Guid>.Success();
        }

        private static Result<Guid> ReturnReasonFail(string reason) => Result<Guid>.Failure(reason);
    }
}
