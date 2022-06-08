using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];


if (modelName == "Computer")
{
    if (modelAction == "List") // roteamento
    {
        System.Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            System.Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
    }

    if (modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }

    if(modelAction == "Delete")
    {
        
        int id = Convert.ToInt32(args[2]);
        computerRepository.Delete(id);
    }

    if(modelAction == "Update")
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);

        computerRepository.Update(computer);
    }

    if(modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);

            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
        else
        {
            System.Console.WriteLine($"O computador {id} não existe");
        }



    }
}

if (modelName == "Lab")
{
    if (modelAction == "List")
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Lab;";

         var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    "{0}, {1}, {2}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)
                    );
            }
        connection.Close();
    }
    if (modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        int number = Convert.ToInt32(args[3]);
        string name = args[4];
        string block = args[5];

        
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Lab VALUES($id, $number, $name, $block);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);
        command.ExecuteNonQuery();
        connection.Close();
    }

    if(modelAction == "Delete")
    {
        int id = Convert.ToInt32(args[2]);
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText ="DELETE FROM Lab WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();

        connection.Close();
    }
}