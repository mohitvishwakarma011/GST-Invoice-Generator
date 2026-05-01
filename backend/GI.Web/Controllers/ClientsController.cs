using GI.Application.Features.Clients.CreateClient;
using GI.Application.Features.Clients.DeleteClient;
using GI.Application.Features.Clients.GetClients;
using MediatR;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetClientsQuery(UserId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteClientCommand(ClientId: id, UserId: UserId));
            return NoContent();
        }
    }
}
