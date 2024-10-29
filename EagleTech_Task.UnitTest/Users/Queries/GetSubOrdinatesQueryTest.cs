using EagleTech_Task.Application.Features.Users.Commands.Create;
using EagleTech_Task.Application.Features.Users.Queries.GetSubOrdinates;
using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using FluentAssertions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.UnitTest.Users.Queries
{
    public class GetSubOrdinatesQueryTest:TestBase
    {
        [Fact]
        public async Task Handler_WhenGetSubOrdinates_ReturnSuccess()
        {
            //Arrange
            var unitOfWork = GetUnitOfWork();

            var roles = await unitOfWork.Repository<Role>().GetAllAsync();

            var admin= await RegisterUser(new()
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
                ManagerId=admin.Data
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
            var handler = new GetSubOrdinatesQueryHandler(unitOfWork);
            var result = await handler.Handle(new(admin.Data), default);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
