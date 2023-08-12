using Gaming.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGamingModule(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();