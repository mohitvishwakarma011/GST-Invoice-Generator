using GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice;
using MediatR;
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
        public async Task<IActionResult> CreateInvoice([FromBody]CreateInvoiceCommand command)
        {
            command.UserId = UserId;
            return Ok(await mediator.Send(command));
        }
    }
}
