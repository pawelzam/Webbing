using Microsoft.EntityFrameworkCore;
using Webbing.Assignment.Service.Entities;
using Webbing.Assignment.Service.Interfaces;
using Webbing.Assignment.Service.Models;

namespace Webbing.Assignment.Service;
public class Repository : IRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task CreateUsageAsync(Usage usage, CancellationToken cancellationToken = default)
    {
        _applicationDbContext.Usages.Add(usage);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateUsagesAsync(IEnumerable<Usage> usages, CancellationToken cancellationToken = default)
    {
        _applicationDbContext.Usages.AddRange(usages);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Usage>> GetUsageForNetworkEventsAsync(CancellationToken cancellationToken = default)
    {
        var query = from networkEvent in _applicationDbContext.NetworkEvents
                    join sim in _applicationDbContext.Sims on networkEvent.SimId equals sim.Id
                    join customer in _applicationDbContext.Customers on sim.CustomerId equals customer.Id
                    select new Usage
                    {
                        CustomerId = sim.CustomerId,
                        CustomerName = customer.Name,
                        Date = networkEvent.CreatedOnUtc.Date,
                        SimId = networkEvent.SimId,
                        Quota = networkEvent.Quota
                    };

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<UsagesGroupByCustomer> GetUsagesGroupByCustomerAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _applicationDbContext.Usages
            .Where(u => u.Date >= fromDate && u.Date <= toDate) // Ensure to filter by date
            .GroupBy(u => new { u.CustomerId, u.CustomerName })
            .Select(g => new
            {
                g.Key.CustomerId,
                g.Key.CustomerName,
                TotalQuota = g.Sum(u => u.Quota) / (1024 * 1024),
                LastUsageDate = g.Max(u => u.Date),
                SimIds = g.Select(u => u.SimId).Distinct()
            })
            .ToListAsync(cancellationToken);

        var customerInfos = result.Select(g => new CustomerInfo(
            g.CustomerId,
            g.CustomerName,
            g.SimIds.Count(),
            g.TotalQuota,
            g.LastUsageDate
        ))
        .OrderBy(p => p.Quota)
        .Take(2)
        .ToList();

        return new UsagesGroupByCustomer(customerInfos);
    }

    public async Task<UsagesGroupBySim> GetUsagesGroupBySimAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _applicationDbContext.Usages
            .GroupBy(u => u.SimId)
            .Select(g => new
            {
                SimId = g.Key,
                Count = g.Count(),
                Quota = g.Sum(u => u.Quota) / (1024 * 1024)
            })
            .ToListAsync();

        var totalCount = result.Count;
        var totalQuota = result.Sum(r => r.Quota);

        return new UsagesGroupBySim(totalCount, totalQuota);
    }
}
