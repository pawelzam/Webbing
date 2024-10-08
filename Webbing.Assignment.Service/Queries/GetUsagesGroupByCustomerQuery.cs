using MediatR;
using Webbing.Assignment.Service.Interfaces;
using Webbing.Assignment.Service.Models;

namespace Webbing.Assignment.Service.Queries;
public record GetUsagesGroupByCustomerQuery(DateTime FromDate, DateTime ToDate) : IRequest<UsagesGroupByCustomer>;

public class GetUsagesGroupByCustomerQueryHandler : IRequestHandler<GetUsagesGroupByCustomerQuery, UsagesGroupByCustomer>
{
    private readonly IRepository _repository;

    public GetUsagesGroupByCustomerQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsagesGroupByCustomer> Handle(GetUsagesGroupByCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetUsagesGroupByCustomerAsync(request.FromDate, request.ToDate, cancellationToken);
    }
}