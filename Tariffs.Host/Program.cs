using System.Reflection;
using Microsoft.OpenApi.Models;
using Tariffs.Host;
using Tariffs.Host.Providers.Springfield;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Tariff API",
        Description = "Tariff API"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddControllers();
builder.Services.AddTariffs();
builder.Services.AddSpringfield();
// TODO: add logging
// TODO: add global exception handling

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.EnableTryItOutByDefault());
}

app.MapControllers().WithOpenApi();

app.Run();
