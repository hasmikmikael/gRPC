using Grpc.Net.Client;
using GreeterClientApp;

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7069");

// creating a client 
var client = new Greeter.GreeterClient(channel);
Console.Write("Enter name: ");
var name = Console.ReadLine();

// exchanging messages with the server 
var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine($"Server response: {reply.Message}");
Console.ReadKey();