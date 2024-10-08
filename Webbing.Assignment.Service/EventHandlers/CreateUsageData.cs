using MediatR;
using Microsoft.Extensions.Logging;
using Webbing.Assignment.Service.Events;
using Webbing.Assignment.Service.Interfaces;

namespace Webbing.Assignment.Service.EventHandlers;
public class CreateUsageData : IRequestHandler<NetworkEventsReceived>
{
    private readonly IRepository _repository;
    
    public CreateUsageData(IRepository repository, ILogger<CreateUsageData> logger)
    {
        _repository = repository;
    }

    public async Task Handle(NetworkEventsReceived @event, CancellationToken cancellationToken)
    {
        await _repository.CreateUsagesAsync(@event.Usages, cancellationToken);
    }
}
