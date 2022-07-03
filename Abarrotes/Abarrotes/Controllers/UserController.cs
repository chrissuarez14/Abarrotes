using Abarrotes.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abarrotes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        [HttpPost("login")]
       public  IActionResult Autetificar([FromBody] AuthRequest oModel )
        {
            return Ok(oModel);
        }
    }
}
