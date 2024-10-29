using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Orders.Commands.UpdateStatus
{
    public record UpdateOrderStatusCommand:IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }

    internal class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateOrderStatusCommand> _validator;

        public UpdateOrderStatusCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<UpdateOrderStatusCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<string>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var validation=await _validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                return Result<string>.ValidationFailure(validation.Errors, HttpStatusCode.UnprocessableEntity);
            }

            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(command.Id);

            if (order == null)
            {
                return Result<string>.Failure("Order not found.", HttpStatusCode.NotFound);
            }

            order.Status = command.Status;

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result<string>.Success("Order status updated.");
        }
    }
}
