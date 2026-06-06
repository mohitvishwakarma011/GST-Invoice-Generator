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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] GetClientsQuery query)
        {
            query.UserId = UserId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateClientCommand request)
        {
            request.UserId = UserId;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteClientCommand(ClientId: id, UserId: UserId));
            return NoContent();
        }
    }
}
