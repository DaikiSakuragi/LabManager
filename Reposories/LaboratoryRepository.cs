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

     private Laboratory ReaderToLaboratory(SqliteDataReader reader)
    {
        var laboratory = new Laboratory(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2),  reader.GetString(3));

        return laboratory;
    }
}