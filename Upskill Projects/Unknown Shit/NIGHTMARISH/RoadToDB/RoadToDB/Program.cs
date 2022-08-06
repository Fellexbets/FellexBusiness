using System;

namespace RoadToDB
{
    // More info: 
    //      Attribute:   https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/
    //      Foreach:     https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/make-class-foreach-statement
    //      IEnumerable: https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-5.0
    //      Generics:    https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/

    // Draft: Este programa é apenas um rascunho de possível trabalho futuro.
    public class Program
    {
        static void Main(string[] args)
        {
            bool terminar = false;
            do
            {
                ICrud<Entity> selected = RoadToDBConsole.GetAllEntities();
                int selectedOperation = RoadToDBConsole.GetAllOperations();

                switch (selectedOperation)
                {
                    case (int)Operation.CREATE:
                        Entity toCreate = RoadToDBConsole.CreateNewEntityFromConsoleInput(selected);
                        selected.Add(toCreate); 
                        // FileUtils.WriteCustomersToHtml();
                        break;
                    case (int)Operation.READ:
                        Console.WriteLine(selected.ToString());
                        break;
                    case (int)Operation.UPDATE:
                        Entity toUpdate = RoadToDBConsole.CreateNewEntityFromConsoleInput(selected);
                        selected.Update(toUpdate);
                        break;
                    case (int)Operation.DELETE:
                        Console.WriteLine("Insert id to delete");
                        string idToRemove = Console.ReadLine();
                        selected.Remove(idToRemove);
                        break;
                    case (int)Operation.SAVE:
                        selected.SaveChanges();
                        break;
                    case (int)Operation.FIND:
                        Console.WriteLine("Insert id to find");
                        string idToFind = Console.ReadLine();
                        selected.Find(idToFind);
                        break;
                    case (int)Operation.CLOSE:
                        terminar = true;
                        break;
                    default:
                        Console.WriteLine("Wrong operation");
                        break;
                }
            }
            while (!terminar);
        }
    }
}
