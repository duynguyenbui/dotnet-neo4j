using GraphDatabase.API.Apis;
using GraphDatabase.API.Extensions;
using GraphDatabase.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

// Configure Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapGraphDatabaseApi();

app.Run();