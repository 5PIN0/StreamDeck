using MovliAPI.Models;
using MySql.Data.MySqlClient;

namespace MovliAPI.Services
{
    public class BotonesService
    {
        private readonly string _connectionString;

        public BotonesService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public List<Boton> ObtenerBotones()
        {
            var botones = new List<Boton>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM botones", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                botones.Add(new Boton
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Funcion = reader.IsDBNull(reader.GetOrdinal("Funcion")) ? null : reader.GetString("Funcion"),
                    FechaAsignacion = reader.GetDateTime("FechaAsignacion")
                });
            }

            return botones;
        }

        public Boton ObtenerBotonPorId(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM botones WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Boton
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Funcion = reader.IsDBNull(reader.GetOrdinal("Funcion")) ? null : reader.GetString("Funcion"),
                    FechaAsignacion = reader.GetDateTime("FechaAsignacion")
                };
            }

            return null;
        }

        public Boton ObtenerBotonPorNombre(string nombreBoton)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM botones WHERE Nombre = @nombreBoton", connection);
            command.Parameters.AddWithValue("@nombreBoton", nombreBoton);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Boton
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Funcion = reader.IsDBNull(reader.GetOrdinal("Funcion")) ? null : reader.GetString("Funcion"),
                    FechaAsignacion = reader.GetDateTime("FechaAsignacion")
                };
            }

            return null;
        }

        public void CrearBoton(Boton boton)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("INSERT INTO botones (Nombre, Funcion, FechaAsignacion) VALUES (@nombre, @funcion, @fecha)", connection);
            command.Parameters.AddWithValue("@nombre", boton.Nombre);
            command.Parameters.AddWithValue("@funcion", boton.Funcion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@fecha", DateTime.Now);
            command.ExecuteNonQuery();
        }

        public void ActualizarBoton(int id, Boton boton)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("UPDATE botones SET Nombre = @nombre, Funcion = @funcion WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nombre", boton.Nombre);
            command.Parameters.AddWithValue("@funcion", boton.Funcion ?? (object)DBNull.Value);
            command.ExecuteNonQuery();
        }

        public void EliminarBoton(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("DELETE FROM botones WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public bool ExisteFuncion(string nombreFuncion)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT COUNT(*) FROM funciones WHERE Nombre = @nombreFuncion", connection);
            command.Parameters.AddWithValue("@nombreFuncion", nombreFuncion);

            var count = (long)command.ExecuteScalar();
            return count > 0;
        }
    }
}
