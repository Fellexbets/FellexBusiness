using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    public class Operations
    {
        public static void Close()
        {
            Program.EndProgram = true;
        }

        public static void Save()
        {
            ProgramOperations.SelectEntities();
            ProgramOperations.SelectedEntity.SaveChanges();
        }

        public static void SaveAll()
        {
            foreach (KeyValuePair<int, ICrud<Entity>> ele1 in ProgramOperations.EntitiesDict)
            {
                ele1.Value.SaveChanges();
            }
        }

        public static void Find()
        {
            ProgramOperations.SelectEntities();
            Console.WriteLine("Insert id to find");
            string idToFind = Console.ReadLine();
            Entity entity = ProgramOperations.SelectedEntity.Find(idToFind);
            Console.WriteLine(entity);
        }

        public static void Delete()
        {
            ProgramOperations.SelectEntities();
            Console.WriteLine("Insert id to delete");
            string idToRemove = Console.ReadLine();
            ProgramOperations.SelectedEntity.Remove(idToRemove);
        }

        public static void Update()
        {
            ProgramOperations.SelectEntities();
            Entity toUpdate = ProgramOperations.CreateNewEntityFromConsoleInput(ProgramOperations.SelectedEntity);
            ProgramOperations.SelectedEntity.Update(toUpdate);
        }

        public static void Read()
        {
            ProgramOperations.SelectEntities();
            Console.WriteLine(ProgramOperations.SelectedEntity.Read());
        }

        public static void Create()
        {
            ProgramOperations.SelectEntities();
            Entity toCreate = ProgramOperations.CreateNewEntityFromConsoleInput(ProgramOperations.SelectedEntity);
            ProgramOperations.SelectedEntity.Add(toCreate);
            // FileUtils.WriteCustomersToHtml();
        }
    }
}
