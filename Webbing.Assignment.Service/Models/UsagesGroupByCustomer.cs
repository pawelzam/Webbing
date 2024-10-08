using System.ComponentModel;

namespace Webbing.Assignment.Service.Models;
public record UsagesGroupByCustomer(IEnumerable<CustomerInfo> topCustomers);

public record CustomerInfo(Guid CustomerId, string CustomerName, int SimsCount, long Quota, DateTime LastUsage);