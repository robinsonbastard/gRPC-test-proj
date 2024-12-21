using System.Threading.Channels;
using Grpc.Core;
using GrpcProto.LoveContract;

namespace Grpc.Love.gRpc;

public sealed class LoveGrpcService : GrpcProto.LoveContract.Love.LoveBase
{
    public override Task<IsSheLoveReply> IsSheLove(IsSheLoveRequest request, ServerCallContext context)
    {
        Console.WriteLine($"Пришел запрос от Hello на опредление любови {request.Name}");
        
        var result = IsSheLove(request.Name);

        return Task.FromResult(new IsSheLoveReply
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
            _ => throw new RpcException(new Status(StatusCode.InvalidArgument, $"Не знаю {text}", 
                new InvalidOperationException($"Не знаю {text}")))
        };
    }
}