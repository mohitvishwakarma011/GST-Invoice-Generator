using GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice;
using GI.Application.Features.InvoiceWorkItem.Commands.DeleteInvoice;
using GI.Application.Features.InvoiceWorkItem.Queries.GetInvoiceById;
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetInvoiceById([FromRoute]int id)
        {
            var query = new GetInvoiceByIdQuery { InvoiceId = id, UserId = UserId};
            return Ok(await mediator.Send(query));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int id)
        {
            var query = new DeleteInvoiceCommand { UserId = UserId, InvoiceId = id };
            await mediator.Send(query);
            return NoContent();
        }
    }
}
