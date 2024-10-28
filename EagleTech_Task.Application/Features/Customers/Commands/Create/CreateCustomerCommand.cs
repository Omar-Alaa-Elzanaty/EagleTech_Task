using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Customers.Commands.Create
{
    public record CreateCustomerCommand:IRequest<Result<Guid>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = command.Adapt<Customer>();

            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result<Guid>.Success(customer.Id, "Customer added successfully.");
        }
    }
}
