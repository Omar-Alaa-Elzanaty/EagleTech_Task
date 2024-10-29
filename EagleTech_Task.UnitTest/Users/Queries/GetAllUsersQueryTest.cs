using EagleTech_Task.Application.Features.Users.Queries.GetAll;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Users.Queries
{
    public class GetAllUsersQueryTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenGetAllUsers_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            var adminRole = await unitOfWork.Repository<Role>().Entities.FirstAsync(x => x.Name == Constants.Admin);

            await RegisterUser(new()
            {
                Email = "omar@gmail.com",
                FirstName = "omar",
                LastName = "Alaa",
                Password = "123@Abc",
                UserName = "OmarTest",
                RoleId = adminRole.Id
            }, unitOfWork);

            //Act
            var handler = new GetAllUsersQueryHandler(unitOfWork);
            var result = await handler.Handle(new(), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
