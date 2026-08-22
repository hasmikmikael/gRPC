using Crud;
using Grpc.Core;
using Grpc.Net.Client;

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7182");

// creating a client 
var client = new UserService.UserServiceClient(channel);

// retrieving a list of objects 
ListReply users = await client.ListUsersAsync(new
       Google.Protobuf.WellKnownTypes.Empty());

foreach (var user in users.Users)
{
    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}
Console.WriteLine("");


try
{
    // getting one object by id = 2 
    UserReply user = await client.GetUserAsync(new GetUserRequest
        { Id = 2 });
    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status.Detail); // getting the response status 
}
Console.WriteLine("");


// adding a single object 
UserReply userR = await client.CreateUserAsync(new CreateUserRequest
    { Name = "Alice", Age = 32 });
Console.WriteLine($"{userR.Id}. {userR.Name} - {userR.Age}");
Console.WriteLine("");