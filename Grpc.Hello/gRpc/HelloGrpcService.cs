using Grpc.Core;
using GrpcProto.HelloContract;

namespace Grpc.Hello.gRpc;

public sealed class HelloGrpcService : GrpcProto.HelloContract.Hello.HelloBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        if (request.Name != "Роман")
            return Task.FromResult(new HelloReply
            {
                Message = "Привет " + request.Name
            });
        
        var ex = new InvalidOperationException("Романов не преветствуем");
        throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message, ex));
    }
}