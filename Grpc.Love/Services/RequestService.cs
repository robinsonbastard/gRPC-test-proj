using System.Threading.Channels;
using Grpc.Net.Client;
using GrpcProto.HelloContract;

namespace Grpc.Love.Services;

public sealed class RequestService(Channel<string, string> channel)
{
    private readonly ChannelWriter<string> _channelWriter = channel.Writer;

    public async Task RequestAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            using var grpcChannel = GrpcChannel.ForAddress("http://localhost:5028");
            var client = new Hello.HelloClient(grpcChannel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = message },
                cancellationToken: cancellationToken);
            var messageForPrint = $"Получен ответ от gRPC-2 {reply.Message}";
            await _channelWriter.WriteAsync(messageForPrint, cancellationToken);
        }
        catch (Exception e)
        {
            await _channelWriter.WriteAsync("При запросе от Hello" + e.Message, cancellationToken);
        }
    }
}