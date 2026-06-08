using Catalog.API.Endpoints;
using Catalog.API.Endpoints;
using Catalog.API.Middlewares;
using Catalog.Infrastructure;
using CatalogService.Application;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.MapGet("/health", () =>
{
    return Results.Ok("Catalog API Healthy");
});
//.WithOpenApi();
app.UseMiddleware<ExceptionMiddleware>();
app.MapProductEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
