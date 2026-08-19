using Grpc.Net.Client;
using Messenger2ClientApp;

// shipping details 
string[] messages = { "Hello", "How are you?", "Why aren't you saying anything?", "Are you asleep or what?", "Well, bye for now" }; 

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7161");

// creating a client 
var client = new Messenger.MessengerClient(channel);

var call = client.ClientDataStream();

// sending every message 
foreach (var message in messages)
{
    await call.RequestStream.WriteAsync(new Request
    { Content = message });
}

// finishing sending messages in the stream 
await call.RequestStream.CompleteAsync();

// receiving a server response 
Response response = await call.ResponseAsync;
Console.WriteLine($"Server response: {response.Content}");