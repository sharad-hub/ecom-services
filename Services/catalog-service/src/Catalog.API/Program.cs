using Catalog.API.Endpoints;
using Catalog.API.Middlewares;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Persistence;
using CatalogService.Application;
using Serilog;

Log.Logger =
    new LoggerConfiguration()
        .ReadFrom.Configuration(
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
        .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
        builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Catalog:";
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddHealthChecks()
    .AddNpgSql(
        builder.Configuration
            .GetConnectionString("CatalogDb")!);

Console.WriteLine("Building app");

var app = builder.Build();

Console.WriteLine("App built");

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Starting seed");

    var context =
        scope.ServiceProvider
            .GetRequiredService<CatalogDbContext>();

    await CatalogDbContextSeeder.SeedAsync(context);

    Console.WriteLine("Seed complete");
}

Console.WriteLine("Starting web host");

 
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

// app.MapGet("/health", () =>
// {
//     return Results.Ok("Catalog API Healthy");
// });
app.MapHealthChecks("/health");
Console.WriteLine("health");

//.WithOpenApi();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging(); 
app.MapProductEndpoints();
Console.WriteLine("UseMiddleware");

app.Run();

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
