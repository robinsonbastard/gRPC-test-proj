using Grpc.Core;

namespace Grpc.Server.Services;

public sealed class CalculatorService (ILogger<CalculatorService> logger) : Calculator.CalculatorBase
{
    public override Task<CalculateResponse> Calculate(CalculateRequest request, ServerCallContext context)
    {
        var result = request.OperationType switch
        {
            OperationType.Add => request.FirstNum + request.SecondNum,
            OperationType.Multiply => request.FirstNum * request.SecondNum,
            OperationType.Substract => request.FirstNum - request.SecondNum,
            OperationType.Divide => request.FirstNum / request.SecondNum,
            _ => CreateException()
        };

        return Task.FromResult(new CalculateResponse
        {
            Result = result
        });
    }

    private static float CreateException()
    {
        var ex = new InvalidOperationException("Нет такой операции");
        throw new RpcException(new Status(StatusCode.Unknown, ex.Message, ex));
    }
}
