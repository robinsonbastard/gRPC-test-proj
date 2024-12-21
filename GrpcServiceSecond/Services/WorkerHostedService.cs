namespace GrpcServiceSecond.Services;

public sealed class WorkerHostedService(InputDataService inputDataService, PrintService printService) 
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => inputDataService.WorkAsync(cancellationToken), cancellationToken);
        Task.Run(() => printService.PrintAsync(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }
}