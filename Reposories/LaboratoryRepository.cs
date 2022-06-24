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

        var laboratories = connection.Query<Laboratory>("SELECT * FROM Laboratorys");
        
        return laboratories;
    }

    public Laboratory Save(Laboratory laboratory)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Laboratorys VALUES(@Id, @Number, @Name, @Block);", laboratory);

        return laboratory;
    }

    public Laboratory Update(Laboratory laboratory)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Laboratorys SET number = @Number, name = @Name, block = @Block WHERE id = @Id", laboratory);

        return laboratory;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Laboratorys WHERE id = (@Id)", new { Id = id });

    }

    public Laboratory GetById(int id)
    {

        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var laboratories = connection.QuerySingle<Laboratory>("SELECT * FROM Laboratorys WHERE id = @Id", new{ Id = id });

        return laboratories;
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var laboratories = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Laboratorys WHERE id = @Id", new{ Id = id });

        return laboratories;
    }
    
}