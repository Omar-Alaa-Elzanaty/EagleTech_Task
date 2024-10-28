using EagleTech_Task.Api;
using EagleTech_Task.Application.Extensions;
using EagleTech_Task.Infrastructure.Extenstions;
using EagleTech_Task.Presistance.Extensions;
using EagleTech_Task.Presistance.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresistance(builder.Configuration);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.DependencyInjectionService(builder.Configuration);


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseCors(cores => cores.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
DataSeeding.Initialize(app.Services.CreateScope().ServiceProvider);

app.Run();
