using Grpc.Core;

namespace Grpc.Server.Services;

public class HelloService : Hello.HelloBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        Console.WriteLine(request.Name);
        if (request.Name != "Роман")
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        
        var ex = new InvalidOperationException("Романов не преветствуем");
        throw new RpcException(new Status(StatusCode.Unknown, ex.Message, ex));
    }
}