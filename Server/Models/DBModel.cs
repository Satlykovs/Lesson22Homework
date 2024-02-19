namespace Server;

using System.Data.SQLite;
using System.Collections.Generic;


public class SQLiteStoreRepository : IStoreRepository
{

    private string _connectionString;
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Games (
            Id INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            Price REAL NOT NULL,
            Description TEXT NOT NULL,
            Developer TEXT NOT NULL,
            Publisher TEXT NOT NULL
        )";

    public SQLiteStoreRepository(string connectionString)
    {
        _connectionString = connectionString;
        CreateDatabase();
    }

    private void CreateDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString);
        connection.Open();
        using(SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection))
        {    
            Console.WriteLine($"База данных : {_connectionString} создана.");
            command.ExecuteNonQuery();
        }
    }
 
    public List<Game> GetAllGames()
    {
        List<Game> games = new List<Game>();
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Games";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game game = new Game(reader["Name"].ToString(), Convert.ToDouble(reader["Price"]),
                         reader["Description"].ToString(), reader["Developer"].ToString(), reader["Publisher"].ToString());

                        games.Add(game);

                    }
                } 
            }
        }
        return games;
    }

    public void AddGame(Game game)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Games (Name, Price, Description, Developer, Publisher) " +
             "VALUES (@Name, @Price, @Description, @Developer, @Publisher)";
            
            using(SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", game.Name);
                command.Parameters.AddWithValue("@Price", game.Price);
                command.Parameters.AddWithValue("@Description", game.Description);
                command.Parameters.AddWithValue("@Developer", game.Developer);
                command.Parameters.AddWithValue("@Publisher", game.Publisher);
            
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteGame(int id)
    {
        using(SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Games WHERE Id = @Id";
            
            using(SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}