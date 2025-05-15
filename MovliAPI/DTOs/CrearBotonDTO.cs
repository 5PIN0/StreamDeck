using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MovliAPI.DTOs
{
    public class CrearBotonDTO
    {
        [Required]
        [SwaggerSchema("Nombre del botón", Nullable = false)]
        public string Nombre { get; set; }

        [SwaggerSchema("Función del botón", Nullable = true)]
        public string? Funcion { get; set; }
    }
}
