using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace EagleTech_Task.Application.Features.Products.Commands.Create
{
    public record CreateProductCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public double Price { get; set; }
    }

    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateProductCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result<Guid>.ValidationFailure(validationResult.Errors, HttpStatusCode.UnprocessableEntity);
            }

            var product = command.Adapt<Product>();

            await _unitOfWork.Repository<Product>().AddAsync(product);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result<Guid>.Success(product.Id, "Product added successfully.");
        }
    }
}
