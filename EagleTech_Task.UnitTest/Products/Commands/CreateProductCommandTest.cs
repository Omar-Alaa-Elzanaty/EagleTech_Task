using EagleTech_Task.Application.Features.Products.Commands.Create;
using EagleTech_Task.Application.Features.Products.Queries.GetAllProducts;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Products.Commands
{
    public class CreateProductCommandTest : TestBase
    {
        [Fact]
        public async Task Handler_WhenCreateProduct_ReturnTrue()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            //Act
            var validator = new CreateProductCommandValidator();
            var handler = new CreateProductCommandHandler(unitOfWork, validator);
            var result = await handler.Handle(new()
            {
                BrandName = "Nike",
                Name = "T-shirt",
                Price = 50
            }, default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeEmpty();
        }
    }
}
