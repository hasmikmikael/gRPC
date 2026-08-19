using Grpc.Core;

namespace Messenger4ServiceApp.Services
{
    public class MessengerService : Messenger.MessengerBase
    {
        public override Task<Response> SendMessage(Request request, ServerCallContext context)
        {
            // getting all the request headers 
            foreach (var header in context.RequestHeaders)
            {
                Console.WriteLine($"{header.Key}: { header.Value} "); //getting the header key and value 
            }

            // receiving one header based on the name—User-Agent 
            var userAgent = context.RequestHeaders.GetValue("user-agent");

            // sending the response 
            return Task.FromResult(new Response
            {
                Content = userAgent
            });
        }
    }
}
