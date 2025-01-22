
using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{
    public class PostreRepository : IPostreRepository
    {
        private readonly string _connectionString;

        public PostreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Postre>> GetAllAsync()
        {
            var postres = new List<Postre>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Calorias FROM Postre";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var postre = new Postre
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                Calorias = reader.GetInt32(3)
                            }; 

                            postres.Add(postre);
                            }
                    }
                }
            }
            return postres;
        }

        public async Task<Postre> GetByIdAsync(int id)
        {
            Postre postre = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Calorias FROM Postre WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            postre = new Postre
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double) reader.GetDecimal(2),
                                Calorias = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return postre;
        }

        public async Task AddAsync(Postre postre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Postre (Nombre, Precio, Calorias) VALUES (@Nombre, @Precio, @Calorias)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", postre.Nombre);
                    command.Parameters.AddWithValue("@Precio", postre.Precio);
                    command.Parameters.AddWithValue("@Calorias", postre.Calorias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Postre postre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Postre SET Nombre = @Nombre, Precio = @Precio, Calorias = @Calorias WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", postre.Id);
                    command.Parameters.AddWithValue("@Nombre", postre.Nombre);
                    command.Parameters.AddWithValue("@Precio", postre.Precio);
                    command.Parameters.AddWithValue("@Calorias", postre.Calorias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Postre WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO Postre (Nombre, Precio, Calorias)
                    VALUES 
                    (@Nombre1, @Precio1, @Calorias),
                    (@Nombre2, @Precio2, @Calorias2)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre1", "Tiramisu");
                    command.Parameters.AddWithValue("@Precio1", 12.50);
                    command.Parameters.AddWithValue("@Calorias",200);

                    command.Parameters.AddWithValue("@Nombre2", "Flan de huevo");
                    command.Parameters.AddWithValue("@Precio2", 15.00);
                    command.Parameters.AddWithValue("@Calorias2", 190);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}