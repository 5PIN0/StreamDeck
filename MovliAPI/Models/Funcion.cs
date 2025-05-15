using Swashbuckle.AspNetCore.Annotations;

namespace MovliAPI.Models
{
    public class Funcion
    {
        [SwaggerSchema("ID único de la función (se genera automáticamente)")]
        public int Id { get; set; }
        [SwaggerSchema("Nombre de la función")]
        public string Nombre { get; set; }
        [SwaggerSchema("Acción de la función")]
        public string Accion { get; set; } = string.Empty;
        [SwaggerSchema("Descripción de la función")]
        public string? Descripcion { get; set; }
        [SwaggerSchema("Fecha de Creación de la función (se genera automáticamente)")]
        public DateTime FechaCreacion { get; set; }
        [SwaggerSchema("Fecha de Modificación de la función (se genera automáticamente)")]
        public DateTime FechaModificacion { get; set; }
    }
}
