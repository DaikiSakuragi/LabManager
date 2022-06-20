using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;



var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

var laboratoryRepository = new LaboratoryRepository(databaseConfig);



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
            System.Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
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

    if(modelAction == "Update")
    {
        int id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
            string ram = args[3];
            string processor = args[4];
            var computer = new Computer(id, ram, processor);
            computerRepository.Update(computer);
        }
        else
        {
            Console.WriteLine($"O computador com Id {id} não existe.");
        }
    }

    if(modelAction == "Delete")
    {
        
        int id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"O computador com Id {id} não existe.");
        }        
    }
}

/*-----------------------------------------------------------//------------------------------------------------------------------*/

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        foreach (var laboratory in laboratoryRepository.GetAll())
        {
            System.Console.WriteLine($"{ laboratory.Id}, { laboratory.Number}, {laboratory.Name}, {laboratory.Block}");
        }
    }
}