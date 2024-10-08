using MediatR;
using Webbing.Assignment.Service.Entities;

namespace Webbing.Assignment.Service.Events;
public record NetworkEventsReceived(IEnumerable<Usage> Usages): IRequest;
