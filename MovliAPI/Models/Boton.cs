using Swashbuckle.AspNetCore.Annotations;

namespace MovliAPI.Models
{
    public class Boton
    {
        [SwaggerSchema("ID único del botón (se genera automáticamente)")]
        public int Id { get; set; }
        [SwaggerSchema("Nombre del botón")]
        public string Nombre { get; set; }
        [SwaggerSchema("Función asignada al botón")]
        public string Funcion { get; set; }
        [SwaggerSchema("Fecha de Asignación de la función al botón (se genera automáticamente)")]
        public DateTime FechaAsignacion { get; set; }
    }
}
