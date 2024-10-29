using EagleTech_Task.Application.Features.Orders.Queries.GetOrdersTotal;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;

namespace EagleTech_Task.UnitTest.Orders.Queries
{
    public class GetOrdersTotalQuery : TestBase
    {
        [Fact]
        public async Task Handler_WhenGetOrdersTotal_ReturnSuccess()
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

            var product2 = new Product()
            {
                BrandName = "Abc",
                Name = "Product2",
                Price = 233.0f
            };

            await unitOfWork.Repository<Customer>().AddAsync(customer);
            await unitOfWork.Repository<Product>().AddRangeAsync([product, product2]);
            await unitOfWork.SaveAsync(default);

            var order = new Order()
            {
                CustomerId = customer.Id,
                Status = OrderStatus.UnderConfirm,
                TotalCost = 5000,
                Details = [new() {
                    ProductId = product.Id,
                    Quantity=10
                },new(){
                    ProductId= product2.Id,
                    Quantity=10
                }]
            };

            await unitOfWork.Repository<Order>().AddAsync(order);
            await unitOfWork.SaveAsync(default);

            //Act
            var handler = new GetOrdersTotalQueryHandler(unitOfWork);
            var result = await handler.Handle(new(), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.First().TotalAmount.Should().Be(4660);
        }
    }
}
