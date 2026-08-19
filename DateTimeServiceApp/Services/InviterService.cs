using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace DateTimeServiceApp.Services
{
    public class InviterService : Inviter.InviterBase
    {
        public override Task<Response> Invite(Request request, ServerCallContext context)
        {
            // the event is tentatively scheduled to start  
            // the following day  
            var eventDateTime = DateTime.UtcNow.AddDays(1);

            // the duration of the event is approximately 2 hours 
            var eventDuration = TimeSpan.FromHours(2);

            // sending the response 
            return Task.FromResult(new Response
            {
                Invitation = $"{request.Name}, we invite you to attend the event",
                Start = Timestamp.FromDateTime(eventDateTime), 
                Duration = Duration.FromTimeSpan(eventDuration)
            }); 
        } 
    } 
}
