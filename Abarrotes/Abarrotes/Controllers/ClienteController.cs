using Abarrotes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abarrotes.Models.Request;
using Abarrotes.Models.Response;

namespace Abarrotes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (AbarrotesContext db = new AbarrotesContext())
                {
                    var list = db.Clientes.ToList();
                    oRespuesta.Data = list;

                    oRespuesta.Exito = 1;
                    

                }

            }catch(Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
            
            
        }

        /*Metodo Post
         * Agregar clientes para el registro y control de ellos.
         */
        [HttpPost]

        public IActionResult Agregar(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (AbarrotesContext db = new AbarrotesContext())
                {
                    Cliente oCliente = new Cliente();

                    oCliente.Nombre = oModel.Nombre;
                    db.Clientes.Add(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = oCliente;
                }
            }catch(Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);
        }


        /*Metodo Put para actualizar datos de cliente
         */

        [HttpPut]
        public IActionResult Actualizar(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (AbarrotesContext db = new AbarrotesContext())
                {
                    Cliente oCliente = db.Clientes.Find(oModel.Id);
                    oCliente.Nombre = oModel.Nombre;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;

                }

            }catch(Exception ex)
            {
                oRespuesta.Mensaje=ex.Message;  
            }

            return Ok(oRespuesta);

        }

        /*Metodo delete para eliminar un cliente
         */


        [HttpDelete ("{id}")]

        public IActionResult Eliminar(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (AbarrotesContext db = new AbarrotesContext())
                {
                    Cliente oCliente = db.Clientes.Find(id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito=1;


                }
            }catch(Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);

        }
    }
}
