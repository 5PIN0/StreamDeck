using Microsoft.AspNetCore.Mvc;
using MovliAPI.DTOs;
using MovliAPI.Models;
using MovliAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MovliAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionesController : ControllerBase
    {
        private readonly FuncionesService _funcionesService;
        private readonly BotonesService _botonesService;

        public FuncionesController(FuncionesService funcionesService, BotonesService botonesService)
        {
            _funcionesService = funcionesService;
            _botonesService = botonesService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva funcion", Description = "Crea una nueva funcion con una accion y una descripción opcional")]
        public IActionResult CrearFuncuion([FromBody] CrearFuncionDTO funcionDTO)
        {
            try
            {
                var nuevaFuncion = new Funcion
                {
                    Nombre = funcionDTO.Nombre,
                    Accion = funcionDTO.Accion,
                    Descripcion = funcionDTO.Descripcion
                };

                _funcionesService.Add(nuevaFuncion);
                return Ok("Función creada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las funciones", Description = "Obtiene todas las funciones almacenadas en la base de datos")]
        public ActionResult<List<Funcion>> GetAll()
        {
            return Ok(_funcionesService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una función por su ID", Description = "Obtiene una función específica utilizando su ID")]
        public ActionResult<Funcion> GetById([FromRoute] int id)
        {
            var funcion = _funcionesService.GetById(id);
            return funcion == null ? NotFound() : Ok(funcion);
        }

        [HttpGet("abrirenlace/{nombreBoton}")]
        [SwaggerOperation(Summary = "Ejecuta la acción asociada a un botón", Description = "Ejecuta la acción configurada en la tabla de funciones para el botón especificado.")]
        public ActionResult EjecutarAccionDeBotonAbrirEnlace(string nombreBoton)
        {
            var boton = _botonesService.ObtenerBotonPorNombre(nombreBoton);
            if (boton == null) return NotFound("Botón no encontrado.");

            var funcion = _funcionesService.GetByName(boton.Funcion);
            if (funcion == null || string.IsNullOrEmpty(funcion.Accion)) return NotFound("Función no encontrada o sin acción definida.");

            _funcionesService.EjecutarAccion(funcion.Accion);
            return Ok($"Acción '{funcion.Accion}' ejecutada correctamente.");
        }

        [HttpGet("abrir/{nombreBoton}")]
        [SwaggerOperation(Summary = "Abre la aplicacion asociada a un boton", Description = "Ejecuta la acción configurada en la tabla de funciones para el botón especificado.")]
        public ActionResult EjecutarAccionDeBotonAbrirApp(string nombreBoton)
        {
            var boton = _botonesService.ObtenerBotonPorNombre(nombreBoton);
            if (boton == null) return NotFound("Botón no encontrado.");

            var funcion = _funcionesService.GetByName(boton.Funcion);
            if (funcion == null || string.IsNullOrEmpty(funcion.Accion)) return NotFound("Función no encontrada o sin acción definida.");

            _funcionesService.AbrirAplicacion(funcion.Accion);
            return Ok($"Acción '{funcion.Accion}' ejecutada correctamente.");
        }
        [HttpGet("enlace/{nombreFuncion}")]
        [SwaggerOperation(Summary = "Devuelve un enlace para la función", Description = "Devuelve un enlace basado en la acción de la función, para generar un QR.")]
        public ActionResult<string> ObtenerEnlaceParaQR(string nombreFuncion)
        {
            var funcion = _funcionesService.GetByName(nombreFuncion);
            if (funcion == null || string.IsNullOrEmpty(funcion.Accion))
                return NotFound("Función no encontrada o sin acción definida.");

            var enlace = funcion.Accion;
            // Asegura que el enlace tenga el prefijo https://
            if (!enlace.StartsWith("http://") && !enlace.StartsWith("https://"))
            {
                enlace = "https://" + enlace;
            }
            // Añade el prefijo para abrir en Edge
            var enlaceEdge = $"start microsoft-edge:{enlace}";

            return Ok(enlaceEdge);
        }

    }
}
