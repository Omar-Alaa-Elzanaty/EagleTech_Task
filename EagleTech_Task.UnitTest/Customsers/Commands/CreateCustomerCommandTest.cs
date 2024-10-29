using EagleTech_Task.Application.Features.Customers.Commands.Create;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Customsers.Commands
{
    public class CreateCustomerCommandTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenCreateCustomer_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            //Act
            var handler=new CreateCustomerCommandHandler(unitOfWork);
            var result = await handler.Handle(new()
            {
                FirstName = "Omar",
                LastName = "Alaa"
            }, default);

            //Assert
            result.Should().NotBeNull();
            result.Data.Should().NotBeEmpty();
        }
    }
}
