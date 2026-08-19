using Grpc.Core;
using Grpc.Net.Client;
using Messenger3ClientApp;

// shipping details 
string[] messages = { "Hey", "How's it going?", "Why aren't you saying anything?", "Are you asleep or what?", "Alright, bye" };

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7013");

// creating a client 
var client = new Messenger.MessengerClient(channel);

// getting an AsyncDuplexStreamingCall object 
var call = client.DataStream();

var readTask = Task.Run(async () =>
{
    await foreach (var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"Server: {response.Content}");
    }
});

foreach (var message in messages)
{
    await call.RequestStream.WriteAsync(new Request { Content = message });
    Console.WriteLine(message);
    await Task.Delay(2000);
}

// finishing sending messages to the server 
await call.RequestStream.CompleteAsync();
await readTask;