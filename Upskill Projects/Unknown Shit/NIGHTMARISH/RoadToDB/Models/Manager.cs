using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Linq;

namespace RoadToDB
{
    // Representa um conjunto de entidades
    public class Manager<T> : ICrud<Entity>, IEnumerable<T> where T : Entity, IHasPrimaryKey, new()
    {
        private List<T> contents;

        public static Manager<T> Instance { get; private set; }

        static Manager() => Instance = new Manager<T>();

        private Manager() => LoadFromFile();

        private void LoadFromFile()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            string path = "db/files/" + pathAttribute.Path;
            if (!File.Exists(path))
            {
                contents = new List<T>();
            }
            else 
            {
                string jsonString = File.ReadAllText(path);
                contents = JsonSerializer.Deserialize<List<T>>(jsonString);
            }
        }

        public override string ToString()
        {
            List<string> lines = new List<string>();
            foreach (T t in contents)
            {
                lines.Add(t.ToString());
            }
            return String.Join("\n", lines.ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();   
        
        public IEnumerator<T> GetEnumerator() => contents.GetEnumerator();

        public Type TypeOfT() => typeof(T);

        public T CreateNewOfT() => new T();

        public void Add(T obj)
        {
            contents.Add(obj);
        }

        
        public void Remove(string id)
        {
            T toRemove = contents.Where(t => t.GetPrimaryKey() == id).FirstOrDefault();
            if (toRemove != null) 
            { 
                contents.Remove(toRemove); 
            }
        }

        public T Find(string id)
        {
            return contents.Where(t => t.GetPrimaryKey() == id).FirstOrDefault();   
        }


        public void Update(T obj)
        {
            T toUpdate = contents.Where(toUpdate => toUpdate.GetPrimaryKey() == obj.GetPrimaryKey()).FirstOrDefault();
            if(toUpdate != null)
            {
               contents[contents.IndexOf(toUpdate)] = obj;
            }
        }

        public void SaveChanges() 
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            string path = "db_files/" + pathAttribute.Path;
            string contentsString = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, contentsString);
        }

        Entity ICrud<Entity>.CreateNewOfT()
        {
            return this.CreateNewOfT();
        }

        Entity ICrud<Entity>.Find(string id)
        {
            return this.Find(id);
        }

        public void Add(Entity obj)
        {
            this.Add(obj as T);
        }

        public void Update(Entity obj)
        {
            this.Update(obj as T);
        }
    }
}
