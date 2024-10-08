using MediatR;
using Webbing.Assignment.Service.Entities;
using Webbing.Assignment.Service.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Webbing.Assignment.Controllers;

[ApiController]
[Route("api")]
public class UsageController : ControllerBase
{
    private readonly ILogger<UsageController> _logger;
    private readonly IMediator _mediator;

    public UsageController(ILogger<UsageController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("usages-group-by-sim")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsagesGroupBySim(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUsagesGroupBySimQuery(fromDate, toDate), cancellationToken);

        return Ok(result);
    }

    [HttpGet("usages-group-by-customer")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsagesGroupByCustomer(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUsagesGroupByCustomerQuery(fromDate, toDate), cancellationToken);

        return Ok(result);
    }
}
