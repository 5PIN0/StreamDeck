using Microsoft.AspNetCore.Mvc;
using MovliAPI.DTOs;
using MovliAPI.Models;
using MovliAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MovliAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BotonesController : ControllerBase
    {
        private readonly BotonesService _botonesService;

        public BotonesController(BotonesService botonesService)
        {
            _botonesService = botonesService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo botón", Description = "Crea un nuevo botón con un nombre y una función opcional")]
        public IActionResult CrearBoton([FromBody] CrearBotonDTO botonDTO)
        {
            try
            {
                var nuevoBoton = new Boton
                {
                    Nombre = botonDTO.Nombre,
                    Funcion = botonDTO.Funcion,
                    FechaAsignacion = DateTime.Now
                };

                _botonesService.CrearBoton(nuevoBoton);
                return Ok("Botón creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un botón", Description = "Actualiza el nombre y función de un botón existente utilizando su ID")]
        public IActionResult ActualizarBoton(int id, [FromBody] ActualizarBotonDTO actualizarBotonDTO)
        {
            try
            {
                var botonActualizado = new Boton
                {
                    Nombre = actualizarBotonDTO.Nombre,
                    Funcion = actualizarBotonDTO.Funcion
                };

                _botonesService.ActualizarBoton(id, botonActualizado);
                return Ok("Botón actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
