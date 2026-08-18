using Grpc.Net.Client;
using GrpcClientApp;

// list of words for translation 
var words = new List<string>() { "red", "yellow", "green" };

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7178");

// creating a client 
var client = new Translator.TranslatorClient(channel);

// sending each word to the service to obtain a translation 
foreach (var word in words)
{
    // preparing the message for sending 
    Request request = new Request { Word = word };

    // sending a message and receive a response 
    Response response = await client.TranslateAsync(request);

    // outputing the word and its translation 
    Console.WriteLine($"{response.Word} : {response.Translation}");
}