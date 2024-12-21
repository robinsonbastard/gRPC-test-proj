using System.Threading.Channels;
using Grpc.Hello.gRpc;
using Grpc.Hello.Services;

var builder = WebApplication.CreateBuilder(args);

var channel = Channel.CreateUnbounded<string>();

builder.Services.AddSingleton<PrintService>(_ => new PrintService(channel));
builder.Services.AddScoped<RequestService>(_ => new RequestService(channel));
builder.Services.AddSingleton<InputDataService>();
builder.Services.AddHostedService<WorkerHostedService>();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<HelloGrpcService>();

app.MapGet("/", () => "gRPC client 1");
        
var cts = new CancellationTokenSource();
        
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};
await app.RunAsync(cts.Token);