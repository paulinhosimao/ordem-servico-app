using OrdemServico.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ChamadoWorker>();
    })
    .Build();

await host.RunAsync();