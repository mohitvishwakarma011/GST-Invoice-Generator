using GI.Application.Features.Dashboard.Queries.GetDashboardSummary;
using GI.Application.Features.Dashboard.Queries.GetRecentClient;
using GI.Application.Features.Dashboard.Queries.GetRecentInvoiceItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetDashboardItems()
        {
            var query = new GetDashboardSummaryQuery { UserId = UserId };
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("recent-invoices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetRecentInvoices()
        {
            var query = new GetRecentInvoiceItemQuery { UserId = UserId };
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("recent-clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetRecentClients()
        {
            var query = new GetRecentClientQuery{ UserId = UserId };
            return Ok(await _mediator.Send(query));
        }

    }
}
