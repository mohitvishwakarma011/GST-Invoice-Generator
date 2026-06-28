using GI.Application.Features.Seed.SeedCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedController(IHttpContextAccessor accessor, ISender mediator) : BaseController(accessor)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SeedDatabase()
        {
            await mediator.Send(new SeedDbCommand());
            return Ok();
        }


    }
}
