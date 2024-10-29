using EagleTech_Task.Application.Features.Users.Commands.Create;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Users.Commands
{
    public class CreateUserCommandTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenCreateUser_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();
            var authSerivce = GetAuthService();

            var adminRole = await unitOfWork.Repository<Role>().Entities
                            .SingleAsync(x => x.Name == Constants.Admin);

            //Act
            var validation = new CreateUserCommandValidator();
            var handler=new CreateUserCommandHandler(unitOfWork,validation, authSerivce);
            var result=await handler.Handle(new()
            {
                Email = "omar@gmail.com",
                FirstName = "omar",
                LastName = "Alaa",
                Password = "123@Abc",
                UserName = "OmarTest",
                RoleId = adminRole.Id
            }, default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }
    }
}
