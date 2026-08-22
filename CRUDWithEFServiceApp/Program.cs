using CRUDWithEFServiceApp;
using CRUDWithEFServiceApp.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// connection string 
string connStr = "Data Source=grpc.db";

// adding the ApplicationContext as a service to the application. 
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connStr));
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<UserApiService>();
app.MapGet("/", () => "Hello World!");

app.Run();