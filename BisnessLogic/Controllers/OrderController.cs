using Microsoft.AspNetCore.Mvc;

namespace BisnessLogic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {

        }
    }
}
