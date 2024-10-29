using EagleTech_Task.Application.Features.Users.Commands.Create;
using EagleTech_Task.Application.Interfaces.Auth;
using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Constant;
using EagleTech_Task.Infrastructure.Services;
using EagleTech_Task.Presistance;
using EagleTech_Task.Presistance.Repo;
using EagleTeck_Task.Shared;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace EagleTech_Task.UnitTest
{
    public class TestBase : IDisposable
    {
        protected readonly DbContextOptions<OrderMangmentDbContext> _dbContextOptions;
        private readonly WebApplicationBuilder _builder;
        protected readonly ServiceProvider _serviceProvider;
        private readonly IServiceScope _serviceScope;
        private readonly IServiceScopeFactory _scopeFactory;

        public TestBase()
        {

            _dbContextOptions = new DbContextOptionsBuilder<OrderMangmentDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var services = new ServiceCollection();

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

            _builder = WebApplication.CreateBuilder();
            _builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var mockMediator = new Mock<IMediator>();


            AddServices(services, mockWebHostEnvironment, _builder.Configuration, mockMediator);

            _serviceProvider = services.BuildServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();
            _scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();

            InitializeDatabase().GetAwaiter();
        }

        private void AddServices(
            ServiceCollection services,
            Mock<IWebHostEnvironment> mockWebHostEnvironment,
            IConfiguration configuration,
            Mock<IMediator> mockMediator)
        {
            var context = new Mock<IHttpContextAccessor>();

            context.SetupGet(x => x.HttpContext)
                .Returns(new DefaultHttpContext());

            services.AddSingleton(provider =>
            {
                return new OrderMangmentDbContext(_dbContextOptions);
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetAssembly(new Application.Comman.Mapping.OrderDetailMapping().GetType())!);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthServices,AuthService>();
            services.AddSingleton(mockWebHostEnvironment.Object);
            services.AddSingleton(configuration);
            services.AddSingleton(mockMediator.Object);
            services.AddMemoryCache();
        }
        public static Mock<IHttpContextAccessor> GetMockHttpContextAccessor(string userId)
        {
            var identity = new GenericIdentity("User");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            var httpContext = new DefaultHttpContext();
            httpContext.User.AddIdentity(identity);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);
            return httpContextAccessor;
        }
        public IAuthServices GetAuthService()
        {
            return _serviceProvider.GetRequiredService<IAuthServices>();
        }

        public async Task<Result<Guid>> RegisterUser(CreateUserCommand command, IUnitOfWork unitOfWork)
        {
            var authServices = GetAuthService();


            var validation = new CreateUserCommandValidator();
            var handler = new CreateUserCommandHandler(unitOfWork, validation, authServices);

            return await handler.Handle(command, default);
        }

        #region New

        public RoleManager<IdentityRole> GetRoleManager()
        {
            return _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        #endregion

        public IUnitOfWork GetUnitOfWork()
        {
            return _serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public void Dispose()
        {
            var context = _serviceProvider.GetRequiredService<OrderMangmentDbContext>();
            context.Database.EnsureDeleted();
        }
        private async Task InitializeDatabase()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrderMangmentDbContext>();

                await context.Database.EnsureCreatedAsync();
                await context.Roles.AddAsync(new() { Name = Constants.Supplier });
                await context.Roles.AddAsync(new() { Name = Constants.TechnicalSupport });
                await context.Roles.AddAsync(new() { Name = Constants.Admin });

                await context.SaveChangesAsync();
            }
        }
    }
}
