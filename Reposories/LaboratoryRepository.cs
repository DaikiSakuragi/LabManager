using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class LaboratoryRepository
{

    private readonly DatabaseConfig _databaseConfig;

    public LaboratoryRepository(DatabaseConfig databaseConfig)    
    {
        _databaseConfig = databaseConfig;
    }

    public List<Laboratory> GetAll()
    {

        var laboratorys = new List<Laboratory>();
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Laboratorys;";    

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var laboratory = ReaderToLaboratory(reader);
            laboratorys.Add(laboratory);
        }

        connection.Close();
        return laboratorys;
    }

    public void Save(Laboratory laboratory)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Laboratorys VALUES($id, $number, $name, $block);";
        command.Parameters.AddWithValue("$id", laboratory.Id);
        command.Parameters.AddWithValue("$number", laboratory.Number);
        command.Parameters.AddWithValue("$name", laboratory.Name);
        command.Parameters.AddWithValue("$block", laboratory.Block);
        
        command.ExecuteNonQuery();
        connection.Close();
    }

    public Laboratory GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Laboratorys WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();

        reader.Read();
        var laboratory = ReaderToLaboratory(reader);

        connection.Close();
        return laboratory;
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Laboratorys WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);

        var result = Convert.ToBoolean(command.ExecuteScalar());

        connection.Close();

        return result;
    }
     private Laboratory ReaderToLaboratory(SqliteDataReader reader)
    {
        var laboratory = new Laboratory(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2),  reader.GetString(3));

        return laboratory;
    }
}