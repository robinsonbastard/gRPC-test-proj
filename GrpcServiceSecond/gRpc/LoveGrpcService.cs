using Grpc.Core;
using Grpc.Server;

namespace GrpcServiceSecond.gRpc;

public sealed class LoveGrpcService : Love.LoveBase
{
    public override Task<IsSheReply> IsSheLove(IsSheRequest request, ServerCallContext context)
    {
        var result = IsSheLove(request.Name);

        return Task.FromResult(new IsSheReply
        {
            Result = result
        });
    }
    
    private static bool IsSheLove(string text)
    {
        return text switch
        {
            "Саня" => false,
            "Артем" => true,
            "Олег" => false,
            "Данил" => false,
            _ => throw new RpcException(new Status(StatusCode.Unknown, $"Не знаю {text}", 
                new InvalidOperationException($"Не знаю {text}")))
        };
    }
}