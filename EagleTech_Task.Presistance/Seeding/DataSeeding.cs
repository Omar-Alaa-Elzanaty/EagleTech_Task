using EagleTech_Task.Application.Interfaces.Auth;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EagleTech_Task.Presistance.Seeding
{
    public static class DataSeeding
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<OrderMangmentDbContext>();
            var authService = serviceProvider.GetRequiredService<IAuthServices>();

            if (dbContext.Database.GetAppliedMigrations().Count() == 0)
            {
                dbContext.Database.Migrate();

                await dbContext.AddAsync(new Role() { Name = Constants.Admin });
                await dbContext.AddAsync(new Role() { Name = Constants.Supplier });
                await dbContext.AddAsync(new Role() { Name = Constants.TechnicalSupport });

                await dbContext.SaveChangesAsync();

                authService.CreatePasswordHash("123@Abc", out var passwordHash, out var passwordSalt);

                var adminRole = await dbContext.Roles.SingleAsync(x => x.Name == Constants.Admin);

                await dbContext.Users.AddAsync(new()
                {
                    UserName="OmarAlaa",
                    Email = "omar@test.com",
                    FirstName = "omar",
                    LastName = "alaa",
                    PasswordHash = passwordHash,
                    Roles = [adminRole],
                    PasswordSalt = passwordSalt
                });

                await dbContext.SaveChangesAsync();
            }
            else
            {
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
