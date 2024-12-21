namespace Grpc.Server.Services;

public sealed class InputDataService(RequestService requestService)
{
    public async Task WorkAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var text = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(text))
            {
                await requestService.RequestAsync(text, cancellationToken);
            }
            await Task.Delay(10, cancellationToken);
        }
    }
}