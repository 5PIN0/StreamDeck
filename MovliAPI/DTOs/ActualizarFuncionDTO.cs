using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace MovliAPI.DTOs
{
    public class ActualizarFuncionDTO
    {
        [Required]
        [SwaggerSchema("Nombre actualizado de la función", Nullable = false)]
        public string Nombre { get; set; }

        [Required]
        [SwaggerSchema("Acción actualizada de la función", Nullable = false)]
        public string Accion { get; set; }

        [SwaggerSchema("Descripción actualizada de la función", Nullable = true)]
        public string Descripcion { get; set; }
    }
}
