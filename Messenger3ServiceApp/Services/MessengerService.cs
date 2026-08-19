using Grpc.Core;

namespace Messenger3ServiceApp.Services
{
    public class MessengerService : Messenger.MessengerBase
    {
        // messages to be sent 
        string[] messages = { "Hi", "Not bad", "...", "No", "Bye" };

        public override async Task DataStream(IAsyncStreamReader<Request> requestStream,
            IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {

            // reading incoming messages in a background task 
            var readTask = Task.Run(async () =>
            {
                await foreach (Request message in requestStream.ReadAllAsync())
                {
                    Console.WriteLine($"Client: {message.Content}");
                }
            });

            foreach (var message in messages)
            {
                // sending a response until the client closes the stream 
                if (!readTask.IsCompleted)
                {
                    await responseStream.WriteAsync(new Response { Content = message });
                    Console.WriteLine(message);
                    await Task.Delay(2000);
                }
            }

            await readTask; // awaiting completion of the read task 
        }
    }
}
