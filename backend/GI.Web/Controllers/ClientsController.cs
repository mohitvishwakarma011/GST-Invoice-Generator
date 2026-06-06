using GI.Application.Features;
using GI.Application.Features.Clients.CreateClient;
using GI.Application.Features.Clients.DeleteClient;
using GI.Application.Features.Clients.GetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientsController : BaseController
    {
        private readonly ISender _mediator;
        public ClientsController(ISender mediator,IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Create(CreateClientCommand request)
        {
            request.UserId = UserId;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] GetClientsQuery query)
        {
            query.UserId = UserId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteClientCommand(ClientId: id, UserId: UserId));
            return NoContent();
        }
    }
}
