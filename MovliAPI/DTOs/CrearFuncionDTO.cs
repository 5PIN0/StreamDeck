using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace MovliAPI.DTOs
{
    public class CrearFuncionDTO
    {
        [Required]
        [SwaggerSchema("Nombre de la función", Nullable = false)]
        public string Nombre { get; set; }

        [Required]
        [SwaggerSchema("Acción de la función", Nullable = false)]
        public string Accion { get; set; }

        [SwaggerSchema("Descripción de la función", Nullable = true)]
        public string Descripcion { get; set; }
    }
}
