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


try
{
    // updating a single object – let's change the name of the object with id = 1 to Tomas. 
    UserReply user = await client.UpdateUserAsync(new UpdateUserRequest
        { Id = 1, Name = "Tomas", Age = 38 });
    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status.Detail);
}
Console.WriteLine("");


try
{
    // deleting the object with id = 2 
    UserReply user = await client.DeleteUserAsync(new
          DeleteUserRequest { Id = 2 });
    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status.Detail);
}