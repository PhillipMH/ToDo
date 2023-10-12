using Microsoft.Extensions.Configuration;
using ToDo_Domain.Connection;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
