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
        return await _applicationDbContext.NetworkEvents.Select(p=> new Usage
        {
            CustomerId = _applicationDbContext.Sims.First(s=>s.Id == p.SimId).CustomerId,
            Date = p.CreatedOnUtc.Date,
            SimId = p.SimId,
            Quota = p.Quota
        }).ToListAsync(cancellationToken);
    }

    public async Task<UsagesGroupByCustomer> GetUsagesGroupByCustomerAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _applicationDbContext.Usages
            .GroupBy(u => u.CustomerId)
            .Select(g => new
            {
                SimId = g.Key,
                Count = g.Count(),
                Quota = g.Sum(u => u.Quota)
            })
            .ToListAsync();

        var totalCount = result.Count;
        var totalQuota = result.Sum(r => r.Quota);

        return new UsagesGroupByCustomer(totalCount, totalQuota);
    }

    public async Task<UsagesGroupBySim> GetUsagesGroupBySimAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _applicationDbContext.Usages
            .GroupBy(u => u.SimId)
            .Select(g => new
            {
                SimId = g.Key,
                Count = g.Count(),
                Quota = g.Sum(u => u.Quota)
            })
            .ToListAsync();

        var totalCount = result.Count;
        var totalQuota = result.Sum(r => r.Quota);

        return new UsagesGroupBySim(totalCount, totalQuota);
    }
}
