using Microsoft.Extensions.Configuration;
using ToDo_Domain.Connection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var _connectionString = Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddRazorPages().Services.AddSingleton<Connection>(new Connection(_connectionString))
.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(30); });
app.MapGet("/", () => "Hello World!");

app.Run();
