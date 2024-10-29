using EagleTech_Task.Application.Features.Customers.Queries.GetAllCustomers;
using EagleTech_Task.Domain.Models;
using FluentAssertions;

namespace EagleTech_Task.UnitTest.Customsers.Queries
{
    public class GetAllCustomersTestQuery : TestBase
    {
        [Fact]
        public async Task Handler_WhenGetAllCustomers_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            var customer = new Customer()
            {
                FirstName = "omar",
                LastName = "alaa"
            };

            await unitOfWork.Repository<Customer>().AddAsync(customer);
            await unitOfWork.SaveAsync(default);

            //Act
            var handler = new GetAllCustomerQueryHandler(unitOfWork);
            var result = await handler.Handle(new(), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
