using EagleTech_Task.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Orders.Queries
{
    public class GetOrdersByCustomerIdQueryTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenGetOrderbyCustomerId_ReturnSuccess()
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
            var handler = new GetOrdersByCustomerIdQueryHandler(unitOfWork);
            var result = await handler.Handle(new(customer.Id), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
