using GI.Application.Features.StateItem.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class StateController: ControllerBase
    {
        private readonly IMediator _mediator;
        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("states")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStates()
        {
            return Ok(await _mediator.Send(new GetStateQuery()));
        }
    }
}
