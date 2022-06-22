using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class LaboratoryRepository
{

    private readonly DatabaseConfig _databaseConfig;

    public LaboratoryRepository(DatabaseConfig databaseConfig)    
    {
        _databaseConfig = databaseConfig;
    }

    public IEnumerable<Laboratory> GetAll()
    {

        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.Query<Laboratory>("SELECT * FROM Laboratorys");
        
        return result;
    }

    public Laboratory Save(Laboratory laboratory)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Laboratorys VALUES($id, $number, $name, $block);", laboratory);

        return laboratory;
    }

    public Laboratory Update(Laboratory laboratory)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Laboratorys SET number = $number, name = $name, block = $block WHERE id = $id", laboratory);

        return laboratory;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Laboratorys WHERE id = ($id)", new { Id = id });

    }

    public Laboratory GetById(int id)
    {

        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.QuerySingle<Laboratory>("SELECT * FROM Laboratorys WHERE id = $id", new{ Id = id });

        return result;
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Laboratorys WHERE id = $id", new{ Id = id });

        return result;
    }
    
}