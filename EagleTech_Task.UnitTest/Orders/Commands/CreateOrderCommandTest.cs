using EagleTech_Task.Application.Features.Orders.Commands.Create;
using EagleTech_Task.Domain.Models;
using FluentAssertions;

namespace EagleTech_Task.UnitTest.Orders.Commands
{
    public class CreateOrderCommandTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenCreateOrder_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            var customer = new Customer()
            {
                FirstName = "omar",
                LastName = "alaa"
            };

            var product = new Product()
            {
                BrandName = "Abc",
                Name = "Product",
                Price = 233.0f
            };

            await unitOfWork.Repository<Customer>().AddAsync(customer);
            await unitOfWork.Repository<Product>().AddAsync(product);
            await unitOfWork.SaveAsync(default);

            //Act
            var handler=new CreateOrderCommandHandler(unitOfWork);
            var result = await handler.Handle(new()
            {
                CustomerId = customer.Id,
                Items = [new() {
                    ProductId = product.Id,
                    Quantity=100
                }]
            }, default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeEmpty();
        }
    }
}
