using Grpc.Core;
using Grpc.Net.Client;
using MessengerClientApp;

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7090");

// creating a client 
var client = new Messenger.MessengerClient(channel);

// sending an empty message and receiving a set of messages 
var serverData = client.ServerDataStream(new Request());

// getting the server stream 
var responseStream = serverData.ResponseStream;

//// extracting each message from the stream using iterators 
//while (await responseStream.MoveNext(new CancellationToken()))
//{
//    Response response = responseStream.Current;
//    Console.WriteLine(response.Content);
//}

// to simplify data retrieval on the client side, the ReadAllAsync() method can be used
await foreach (var response in responseStream.ReadAllAsync())
{
    Console.WriteLine(response.Content);
}