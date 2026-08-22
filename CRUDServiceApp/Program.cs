using CRUDServiceApp.Services; // service namespace UserApiService 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<UserApiService>();

app.MapGet("/", () => "Hello World!");

app.Run();