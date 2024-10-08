using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Webbing.Assignment.Service.Events;
using Webbing.Assignment.Service.Interfaces;

namespace Webbing.Assignment.Service.Services;

public class NetworkEventDispatcherService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public NetworkEventDispatcherService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository>();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var usages = await repository.GetUsageForNetworkEventsAsync(cancellationToken);
        await mediator.Send(new NetworkEventsReceived(usages), cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
