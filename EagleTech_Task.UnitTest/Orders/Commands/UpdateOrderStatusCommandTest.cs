using EagleTech_Task.Application.Features.Orders.Commands.UpdateStatus;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;

namespace EagleTech_Task.UnitTest.Orders.Commands
{
    public class UpdateOrderStatusCommandTest : TestBase
    {
        [Fact]
        public async Task Handler_WhenUpdateOrderStatus_ReturnSuccess()
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

            var order = new Order()
            {
                CustomerId = customer.Id,
                Status = OrderStatus.UnderConfirm,
                TotalCost = 5000,
                Details = [new() {
                    ProductId = product.Id,
                    Quantity=10
                }]
            };

            await unitOfWork.Repository<Order>().AddAsync(order);
            await unitOfWork.SaveAsync(default);

            //Act
            var validation = new UpdateOrderStatusCommandValidator();
            var handler = new UpdateOrderStatusCommandHandler(unitOfWork, validation);
            var result = await handler.Handle(new() { Id = order.Id, Status = OrderStatus.Confirm }, default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            order.Status = OrderStatus.Confirm;
        }
    }
}
