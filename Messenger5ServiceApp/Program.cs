using Messenger5ServiceApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

// integrating MessengerService into the request processing 
app.MapGrpcService<MessengerService>();
app.MapGet("/", () => "Hello World!");

app.Run();