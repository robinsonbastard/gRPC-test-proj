using System.Threading.Channels;

namespace GrpcServiceSecond.Services;

public sealed class PrintService(Channel<string, string> channel)
{
    private readonly ChannelReader<string> _channelReader = channel.Reader;
    
    public async Task PrintAsync(CancellationToken cancellationToken)
    {
        await foreach (var message in _channelReader.ReadAllAsync(cancellationToken))
        {
            Console.WriteLine(message); 
        }
    }
}