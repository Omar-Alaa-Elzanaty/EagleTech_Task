using EagleTech_Task.Application.Features.Authentication.Queries.Login;
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

namespace EagleTech_Task.UnitTest.Authentication.Queries
{
    public class LoginQueryTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenLogin_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();
            var authService = GetAuthService();

            var adminRole = await unitOfWork.Repository<Role>().Entities
                            .SingleAsync(x => x.Name == Constants.Admin);

            var registerValidtion = new CreateUserCommandValidator();
            var registerHandler = new CreateUserCommandHandler(unitOfWork,registerValidtion,authService);
            var register = await registerHandler.Handle(new()
            {
                Email = "omar@gmail.com",
                FirstName = "omar",
                LastName = "Alaa",
                Password = "123@Abc",
                UserName = "OmarTest",
                RoleId = adminRole.Id
            }, default);


            //Act
            var handler=new LoginQueryHandler(unitOfWork,authService);
            var result = await handler.Handle(new()
            {
                UserName = "OmarTest",
                Password = "123@Abc"
            },default);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }
    }
}
