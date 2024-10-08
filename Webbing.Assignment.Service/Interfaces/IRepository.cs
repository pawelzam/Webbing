using Webbing.Assignment.Service.Entities;
using Webbing.Assignment.Service.Models;

namespace Webbing.Assignment.Service.Interfaces;
public interface IRepository
{
    Task<IEnumerable<Usage>> GetUsageForNetworkEventsAsync(CancellationToken cancellationToken = default);
    Task CreateUsageAsync(Usage usage, CancellationToken cancellationToken = default);
    Task CreateUsagesAsync(IEnumerable<Usage> usages, CancellationToken cancellationToken = default);
    Task<UsagesGroupBySim> GetUsagesGroupBySimAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
    Task<UsagesGroupByCustomer> GetUsagesGroupByCustomerAsync(DateTime FromDate, DateTime ToDate, CancellationToken cancellationToken);
}
