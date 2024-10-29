using EagleTech_Task.Application.Features.Users.Queries.GetManagerUser;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Users.Queries
{
    public class GetManagerUsersQueryTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenGetManagerUsers_ReturnSuccess()
        {
            //Arrang
            var unitOfWork = GetUnitOfWork();

            var roles = await unitOfWork.Repository<Role>().GetAllAsync();

            var admin = await RegisterUser(new()
            {
                Email = "omar@gmail.com",
                FirstName = "omar",
                LastName = "Alaa",
                Password = "123@Abc",
                UserName = "OmarTest",
                RoleId = roles.First(x => x.Name == Constants.Admin).Id
            }, unitOfWork);
            await RegisterUser(new()
            {
                Email = "karim@gmail.com",
                FirstName = "omar",
                LastName = "Alaa",
                Password = "123@Abc",
                UserName = "KarimTest",
                RoleId = roles.First(x => x.Name == Constants.Supplier).Id,
                ManagerId = admin.Data
            }, unitOfWork);
            await RegisterUser(new()
            {
                Email = "mohamed@gmail.com",
                FirstName = "omar",
                LastName = "Alaa",
                Password = "123@Abc",
                UserName = "MohamedTest",
                RoleId = roles.First(x => x.Name == Constants.TechnicalSupport).Id,
                ManagerId = admin.Data
            }, unitOfWork);

            //Act
            var handler = new GetManagerUserQueryHandler(unitOfWork);
            var result = await handler.Handle(new(), default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
