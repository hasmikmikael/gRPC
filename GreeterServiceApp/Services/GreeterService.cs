using Grpc.Core;

namespace GreeterServiceApp.Services
{
    public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("The message is received from {Name}", request.Name);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }

    //public class GreeterService : Greeter.GreeterBase
    //{
    //    private readonly ILogger<GreeterService> _logger;
    //    public GreeterService(ILogger<GreeterService> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    //    {
    //        logger.LogInformation("The message is received from {Name}", request.Name);
    //        return Task.FromResult(new HelloReply
    //        {
    //            Message = "Hello " + request.Name
    //        });
    //    }
    //}
}
