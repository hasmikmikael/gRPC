using Grpc.Net.Client;
using DateTimeClientApp;

// creating a channel for exchanging messages with the server 
// parameter — gRPC server address 
using var channel = GrpcChannel.ForAddress("https://localhost:7257");

// creating a client 
var client = new Inviter.InviterClient(channel);

// sending the name and receiving an invitation to the event 

var response = await client.InviteAsync(new Request { Name = "Tom" });
var eventInvitation = response.Invitation;
var eventDateTime = response.Start.ToDateTime();
var eventDuration = response.Duration.ToTimeSpan();

// output the data to the console 
Console.WriteLine(eventInvitation);
Console.WriteLine($"Start: {eventDateTime.ToString("dd.MM HH:mm")} Duration: { eventDuration.TotalHours} hours"); 