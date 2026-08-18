using GrpcServiceApp.Services; // TranslatorService namespace 

var builder = WebApplication.CreateBuilder(args);

// adding services for working with gRPC 
builder.Services.AddGrpc();

var app = builder.Build();

// integrating TranslatorService into the request processing 
app.MapGrpcService<TranslatorService>();
app.MapGet("/", () => "Hello World!");

app.Run();