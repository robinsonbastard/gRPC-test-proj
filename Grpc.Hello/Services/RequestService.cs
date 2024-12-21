using System.Threading.Channels;
using Grpc.Net.Client;
using GrpcProto.LoveContract;

namespace Grpc.Hello.Services;

public sealed class RequestService
{
    private readonly ChannelWriter<string> _channelWriter;

    public RequestService(Channel<string, string> channel)
    {
        _channelWriter = channel.Writer;
    }
    
    public async Task RequestAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            using var grpcChannel = GrpcChannel.ForAddress("http://localhost:5029");
            var client = new Love.LoveClient(grpcChannel);
            var reply = await client.IsSheLoveAsync(
                new IsSheLoveRequest { Name = message },
                cancellationToken: cancellationToken);
            var result = reply.Result ? "любовен" : "нелибин";
            var messageForPrint = $"Получен ответ от gRPC-Love {message} {result}";
            await _channelWriter.WriteAsync(messageForPrint, cancellationToken);
        }
        catch (Exception e)
        {
            await _channelWriter.WriteAsync("При запросе от Hello" + e.Message, cancellationToken);
        }
    }
}