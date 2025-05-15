using MySql.Data.MySqlClient;
using MovliAPI.Models;
using System.Diagnostics;

namespace MovliAPI.Services
{
    public class FuncionesService
    {
        private readonly string _connectionString;

        public FuncionesService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public List<Funcion> GetAll()
        {
            var funciones = new List<Funcion>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM funciones", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                funciones.Add(new Funcion
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Accion = reader.GetString("Accion"),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString("Descripcion")
                });
            }

            return funciones;
        }

        public Funcion GetById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM funciones WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Funcion
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Accion = reader.GetString("Accion"),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString("Descripcion")
                };
            }

            return null;
        }

        public Funcion GetByName(string nombreFuncion)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM funciones WHERE Nombre = @nombreFuncion", connection);
            command.Parameters.AddWithValue("@nombreFuncion", nombreFuncion);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Funcion
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Accion = reader.GetString("Accion"),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString("Descripcion")
                };
            }

            return null;
        }

        public void Add(Funcion funcion)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("INSERT INTO funciones (Nombre, Accion, Descripcion) VALUES (@nombre, @accion, @descripcion)", connection);
            command.Parameters.AddWithValue("@nombre", funcion.Nombre);
            command.Parameters.AddWithValue("@accion", funcion.Accion);
            command.Parameters.AddWithValue("@descripcion", funcion.Descripcion ?? string.Empty);
            command.ExecuteNonQuery();
        }

        public void Update(int id, Funcion funcion)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("UPDATE funciones SET Nombre = @nombre, Accion = @accion, Descripcion = @descripcion WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nombre", funcion.Nombre);
            command.Parameters.AddWithValue("@accion", funcion.Accion);
            command.Parameters.AddWithValue("@descripcion", funcion.Descripcion ?? string.Empty);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("DELETE FROM funciones WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public void EjecutarAccion(string accion)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c start {accion}",
                UseShellExecute = false
            });
        }


        public void AbrirAplicacion(string accion)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c start {accion}",
                UseShellExecute = false
            });

        }
    }
}
