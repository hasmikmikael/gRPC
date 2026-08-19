using Grpc.Core;

namespace Messenger5ServiceApp.Services
{
    public class MessengerService : Messenger.MessengerBase
    {
        public override async Task<Response> SendMessage(Request request, ServerCallContext context)
        {
            // getting all the request headers 
            foreach (var header in context.RequestHeaders)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");    // getting the header key and value
            }

            // getting a single header, username 
            var username = context.RequestHeaders.GetValue("username");

            // constructing response headers 
            Metadata responseHeaders = new Metadata();
            responseHeaders.Add("secret-code", "123445");

            // writing headlines in response 
            await context.WriteResponseHeadersAsync(responseHeaders);

            // sending the response 
            return await Task.FromResult(new Response { Content = $"Hello {username}!" });
        }
    }
}
