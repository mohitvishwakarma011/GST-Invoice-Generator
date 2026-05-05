using Microsoft.AspNetCore.Mvc;

namespace GI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IHttpContextAccessor accessor) : BaseController(accessor)
    {

    }
}
