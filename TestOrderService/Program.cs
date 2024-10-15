using System.Text.Json;
using System.Text.Json.Serialization;
using TestOrderService;
using TestOrderService.Models;
using TestOrderService.Services;

var builder = WebApplication.CreateBuilder(args);

var apiKey = builder.Configuration["ApiKey"] ?? throw new Exception("API key not configured.");

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //ignore null values in JSON output
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddScoped<OrderPackingService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>(apiKey);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
