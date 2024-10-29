using EagleTech_Task.Application.Features.Products.Queries.GetAllProducts;
using EagleTech_Task.Domain.Models;
using FluentAssertions;

namespace EagleTech_Task.UnitTest.Products.Queries
{
    public class GetAllProductsQueryTest : TestBase
    {
        [Fact]
        public async Task Handler_WhenGetAllProduct_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            var product = new Product()
            {
                BrandName = "Brand",
                Name = "T-Shirt",
                Price = 50.0f
            };

            await unitOfWork.Repository<Product>().AddAsync(product);
            await unitOfWork.SaveAsync(default);

            //Act
            var handler = new GetlAllProductsQueryHandler(unitOfWork);

            var result = await handler.Handle(new(), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
