using GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice;
using GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController(IHttpContextAccessor accessor,ISender mediator) : BaseController(accessor)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateInvoice([FromBody]CreateInvoiceCommand command)
        {
            command.UserId = UserId;
            return Ok(await mediator.Send(command));
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetInvoiceList()
        {
            var query = new GetInvoicesForUserQuery();
            query.UserId = UserId;
            return Ok(await mediator.Send(query));
        }
    }
}
