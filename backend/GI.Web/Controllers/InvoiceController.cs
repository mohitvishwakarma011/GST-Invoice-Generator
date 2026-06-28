using GI.Application.Common.Interfaces;
using GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice;
using GI.Application.Features.InvoiceWorkItem.Commands.DeleteInvoice;
using GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoiceStatus;
using GI.Application.Features.InvoiceWorkItem.Queries.GetDashboardSummary;
using GI.Application.Features.InvoiceWorkItem.Queries.GetInvoiceById;
using GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicePdf;
using GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser;
using GI.Application.Features.InvoiceWorkItem.Queries.GetRecentInvoiceItem;
using GI.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController(IHttpContextAccessor accessor, ISender mediator, IPdfService pdfService) : BaseController(accessor)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            command.UserId = UserId;
            return Ok(await mediator.Send(command));
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetInvoiceList([FromQuery]GetInvoicesForUserQuery query)
        {
            query.UserId = UserId;
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetInvoiceById([FromRoute] int id)
        {
            var query = new GetInvoiceByIdQuery { InvoiceId = id, UserId = UserId };
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

        [HttpPatch("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateInvoiceStatus([FromBody] UpdateInvoiceStatusCommand command)
        {
            command.UserId = UserId;
            return Ok(await mediator.Send(command));
        }

        [HttpPost("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetInvoicePdf([FromBody] GetInvoicePdfQuery query)
        {
            query.UserId = UserId;
            var result = await mediator.Send(query);
            var bytes = pdfService.GenerateInvoicePdf(result);
            return File(bytes, "application/pdf", $"{result.InvoiceNumber}.pdf");
        }

        [HttpGet("recent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetRecentInvoices()
        {
            var query = new GetRecentInvoiceItemQuery { UserId = UserId };
            return Ok(await mediator.Send(query));
        }
    }
}
