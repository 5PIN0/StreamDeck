using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MovliAPI.DTOs
{
    public class ActualizarBotonDTO
    {
        [Required]
        [SwaggerSchema("Nombre actualizado del botón", Nullable = false)]
        public string Nombre { get; set; }

        [SwaggerSchema("Función actualizada del botón", Nullable = true)]
        public string? Funcion { get; set; }
    }
}
