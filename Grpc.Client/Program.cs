using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Server;

Console.WriteLine("hello grpc");

using var channel = GrpcChannel.ForAddress("http://localhost:5157");
var calculatorClient = new Calculator.CalculatorClient(channel);
var helloClient = new Hello.HelloClient(channel);




while (true)
{
    var name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        continue;
    }
    
    if (name == "Конец")
    {
        break;
    }
    
    try
    {
        var reply = await helloClient.SayHelloAsync(new HelloRequest { Name = name });

        Console.WriteLine(reply);

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}



return;

var requests = MakeTestRequests();
foreach (var req in requests)
{
    try
    {
        var response = await calculatorClient.CalculateAsync(req);
        Console.WriteLine($"Got response: {response.Result}");
    }
    catch (RpcException e)
    {
        Console.WriteLine($"Поймал ошибку \"{e.Status.Detail}\" с кодом {e.StatusCode.ToString()}");
    }
}

IEnumerable<CalculateRequest> MakeTestRequests()
{
    return new List<CalculateRequest>
    {
        new()
        {
            FirstNum = 1,
            SecondNum = 2,
            OperationType = OperationType.Add
        },
        new()
        {
            FirstNum = 1,
            SecondNum = 2,
            OperationType = OperationType.Substract
        },
        new()
        {
            FirstNum = 1,
            SecondNum = 2,
            OperationType = OperationType.Multiply
        },
        new()
        {
            FirstNum = 1,
            SecondNum = 2,
            OperationType = OperationType.Divide
        },
        new()
        {
            FirstNum = 1,
            SecondNum = 2,
            OperationType =  (OperationType)5
        }
    };
}
