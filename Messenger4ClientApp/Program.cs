using Grpc.Net.Client;
using Messenger4ClientApp;

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7046");

// creating a client 
var client = new Messenger.MessengerClient(channel);

// sending a message to the server 
using var call = client.SendMessageAsync(new Request());

// receiving a response 
Response response = await call.ResponseAsync;
Console.WriteLine($"Response: {response.Content}");

// retrieving all the headers and output them to the console 
var headers = await call.ResponseHeadersAsync;

foreach (var header in headers)
{
    Console.WriteLine($"{header.Key}: {header.Value}");
}