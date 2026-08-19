using Grpc.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MessengerServiceApp.Services
{
    public class MessengerService : Messenger.MessengerBase
    {
        string[] messages = { "Hello", "How are you?", "Why aren't you saying anything?", "Are you asleep or what?", "Well, bye for now" }; 

    public override async Task ServerDataStream(Request request,

        IServerStreamWriter<Response> responseStream,

        ServerCallContext context)
        {
            foreach (var message in messages)
            {
                await responseStream.WriteAsync(new Response
                { Content = message });

                // to simulate activity, we introduce a 1-second delay 
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
