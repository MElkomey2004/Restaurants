using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configration) =>

	configration.ReadFrom.Configuration(context.Configuration)

); ;


var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestauranSeeder>();

await seeder.Seed();
// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
