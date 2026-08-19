using Grpc.Core;
using Grpc.Net.Client;
using Messenger5ServiceApp;

using var channel = GrpcChannel.ForAddress("https://localhost:7077");

var client = new Messenger.MessengerClient(channel);

// constructing the outgoing headers 
Metadata requestHeaders = new Metadata();

// adding one heading 
requestHeaders.Add("username", "Tom");

// sending a message to the server 
using var call = client.SendMessageAsync(new Request(), requestHeaders);

// receiving a response 
Response response = await call.ResponseAsync;
Console.WriteLine($"Response: {response.Content}");

// retrieving all the headers and output them to the console 
Metadata responseHeaders = await call.ResponseHeadersAsync;

foreach (var header in responseHeaders)
{
    Console.WriteLine($"{header.Key}: {header.Value}");
}