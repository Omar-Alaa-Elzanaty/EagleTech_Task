using EagleTech_Task.Application.Features.Customers.Queries.GetTopFiveCustomers;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;

namespace EagleTech_Task.UnitTest.Customsers.Queries
{
    public class GetTopFiveCustomersQuery : TestBase
    {
        [Fact]
        public async Task Hanlder_WhenGetTopFiveCustomersQuery_ReturnSuccess()
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
                Status = OrderStatus.Delivered,
                TotalCost = 5000,
                Details = [new() {
                    ProductId = product.Id,
                    Quantity=10
                }]
            };
            await unitOfWork.Repository<Order>().AddAsync(order);
            await unitOfWork.SaveAsync(default);

            //Act
            var handler = new GetTopFiveCustomersQueryHandler(unitOfWork);
            var result = await handler.Handle(new(), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
