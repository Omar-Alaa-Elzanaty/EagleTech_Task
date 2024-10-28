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

            if (dbContext.Database.GetAppliedMigrations().Count() == 0)
            {
                dbContext.Database.Migrate();

                await dbContext.AddAsync(new Role() { Name = Constants.Admin });
                await dbContext.AddAsync(new Role() { Name = Constants.Supplier });
                await dbContext.AddAsync(new Role() { Name = Constants.TechnicalSupport });

                await dbContext.SaveChangesAsync();
            }
            else
            {
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
