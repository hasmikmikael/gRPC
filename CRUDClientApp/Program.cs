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

// retrieving a single object by id = 1 
UserReply use = await client.GetUserAsync(new GetUserRequest { Id = 1 });
Console.WriteLine($"{use.Id}. {use.Name} - {use.Age}");
Console.WriteLine("");