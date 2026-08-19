using Grpc.Core;

namespace GrpcServiceApp.Services
{
    public class TranslatorService : Translator.TranslatorBase
    {
        Dictionary<string, string> words = new() { { "red", "красный" }, { "green", "зеленый" }, { "blue", "синий" } };

        public override Task<Response> Translate(Request request, ServerCallContext context)
        {
            // we receive the sent word 
            var word = request.Word;
            Console.WriteLine($" Requested word: {word}");

            // looking it up in the dictionary  
            // and storing the result in the translation variable 
            if (!words.TryGetValue(word, out var translation))
            {
                // if the word is not found 
                translation = "not found";
            }

            // sending the response 
            return Task.FromResult(new Response
            {
                Word = word,
                Translation = translation
            });
        }
    }
}
