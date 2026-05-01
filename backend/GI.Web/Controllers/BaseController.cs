using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GI.Web.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IHttpContextAccessor _contextAccessor;
        protected int UserId { get; init; }
        public BaseController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            var uid = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var parseResult = int.TryParse(uid, out var result);
            if (!parseResult)
            {
                throw new KeyNotFoundException("Invalid UserId in token");
            }
            UserId = result;
        }
    }
}
