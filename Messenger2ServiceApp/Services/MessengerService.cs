using Grpc.Core;

namespace Messenger2ServiceApp.Services
{
    public class MessengerService : Messenger.MessengerBase
    {
        public override async Task<Response> ClientDataStream(IAsyncStreamReader<Request> requestStream,
            ServerCallContext context)
        {
            await foreach (Request request in requestStream.ReadAllAsync())
            {
                Console.WriteLine(request.Content);
            }

            Console.WriteLine("All data received...");
            return new Response { Content = "All data has been received" };
        }
    }
}
