using System.Threading.Channels;
using Grpc.Net.Client;

namespace Grpc.Server.Services;

public sealed class RequestService
{
    private readonly ChannelWriter<string> _channelWriter;
    private readonly Hello.HelloClient _client;

    public RequestService(Channel<string, string> channel)
    {
        _channelWriter = channel.Writer;
        using var grpcChannel = GrpcChannel.ForAddress("http://localhost:5157");
        _client = new Hello.HelloClient(grpcChannel);
    }
    
    public async Task RequestAsync(string message, CancellationToken cancellationToken)
    {
        var reply = await _client.SayHelloAsync(new HelloRequest { Name = message }, cancellationToken: cancellationToken);
        var messageForPrint = $"Получен ответ от gRPC-2 {reply.Message}";
        await _channelWriter.WriteAsync(messageForPrint, cancellationToken);
    }
}