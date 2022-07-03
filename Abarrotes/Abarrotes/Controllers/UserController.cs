using Abarrotes.Models.Request;
using Abarrotes.Models.Response;
using Abarrotes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abarrotes.Controllers
{
    [Route("[controller]")]
    [ApiController]

    
    public class UserController : ControllerBase
    {

        private IUserService _userService; //Aqui ya inyectamos  la inyeccion 



        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost("login")]
       public  IActionResult Autetificar([FromBody] AuthRequest oModel )
        {
            Respuesta ORespuesta = new Respuesta();

            var userresponse = _userService.Auth(oModel);

            //Al controlador le corresponde manejr el null

            if (userresponse == null)
            {
                ORespuesta.Exito = 0;
                ORespuesta.Mensaje = "Usuario o Contraseña incorrecto)";

                return BadRequest(ORespuesta);
                                   
            }

            ORespuesta.Data = userresponse;
            ORespuesta.Exito = 1;

            return Ok(ORespuesta);
        }
    }
}
