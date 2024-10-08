using MediatR;
using Webbing.Assignment.Service.Interfaces;
using Webbing.Assignment.Service.Models;

namespace Webbing.Assignment.Service.Queries;
public record GetUsagesGroupBySimQuery(DateTime FromDate, DateTime ToDate) : IRequest<UsagesGroupBySim>;


public class GetUsagesGroupBySimQueryHandler : IRequestHandler<GetUsagesGroupBySimQuery, UsagesGroupBySim>
{
    private readonly IRepository _repository;

    public GetUsagesGroupBySimQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsagesGroupBySim> Handle(GetUsagesGroupBySimQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetUsagesGroupBySimAsync(request.FromDate, request.ToDate, cancellationToken);
    }
}