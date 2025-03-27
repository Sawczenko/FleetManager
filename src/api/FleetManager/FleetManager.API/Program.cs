using System.Reflection;
using FleetManager.API.Modules.Orders;
using FleetManager.Infrastructure.Data;
using FleetManager.Infrastructure;
using FleetManager.Application;
using FleetManager.BuildingBlocks.Application;
using FleetManager.BuildingBlocks.Infrastructure;
using FleetManager.Modules.Itineraries.Infrastructure;
using FleetManager.Modules.Orders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructureBuildingBlocks(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

List<IModule> modules = new List<IModule>
{
    new OrdersModule(),
    new ItinerariesModule()
};

foreach (var module in modules)
{
    module.InstallModule(builder.Services, builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    foreach (var module in modules)
    {
        
    }
    DataSeeder dataSeeder = new DataSeeder(app.Services);

    await dataSeeder.SeedDataAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapContractorEndpoints();
app.MapControllers();

app.Run();

public partial class Program {}