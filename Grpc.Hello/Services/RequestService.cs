using System.Threading.Channels;
using Grpc.Net.Client;
using GrpcProto.LoveContract;

namespace Grpc.Hello.Services;

public sealed class RequestService
{
    private readonly ChannelWriter<string> _channelWriter;
    private readonly Love.LoveClient _client;

    public RequestService(Channel<string, string> channel)
    {
        _channelWriter = channel.Writer;
        using var grpcChannel = GrpcChannel.ForAddress("http://localhost:5157");
        _client = new Love.LoveClient(grpcChannel);
    }
    
    public async Task RequestAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            var reply = await _client.IsSheLoveAsync(
                new IsSheLoveRequest { Name = message },
                cancellationToken: cancellationToken);
            var result = reply.Result ? "любовен" : "нелибин";
            var messageForPrint = $"Получен ответ от gRPC-Love {message} {result}";
            await _channelWriter.WriteAsync(messageForPrint, cancellationToken);
        }
        catch (Exception e)
        {
            await _channelWriter.WriteAsync(e.Message, cancellationToken);
        }
    }
}