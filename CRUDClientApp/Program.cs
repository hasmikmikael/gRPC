using Crud;
using Grpc.Core;
using Grpc.Net.Client;

// creating a channel for exchanging messages with the server. 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7017");

// creating a client 
var client = new UserService.UserServiceClient(channel);

// obtaining a list 
ListReply users = await client.ListUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());

foreach (var user in users.Users)
{
    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}
Console.WriteLine("");