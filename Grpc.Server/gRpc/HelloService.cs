using Grpc.Core;

namespace Grpc.Server.gRpc;

public sealed class HelloService : Hello.HelloBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        if (request.Name != "Роман")
            return Task.FromResult(new HelloReply
            {
                Message = "Привет " + request.Name
            });
        
        var ex = new InvalidOperationException("Романов не преветствуем");
        throw new RpcException(new Status(StatusCode.Unknown, ex.Message, ex));
    }
}