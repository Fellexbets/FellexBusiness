using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Linq;

namespace RoadToDB
{
    // Representa um conjunto de entidades
    public class Manager<T> : IEnumerable<T> where T : IHasPrimaryKey
    {
        private List<T> contents;

        public static Manager<T> Instance { get; private set; } 

        static Manager() => Instance = new Manager<T>();

        private Manager() => LoadFromFile();

        private void LoadFromFile()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            string path = "db/" + pathAttribute.Path;
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

        public void Add(T t) 
        {
            contents.Add(t);
        }

        public void Remove(string id)
        {
            T toRemove = Find(id);
            if(toRemove != null)
            { 
                contents.Remove(toRemove);
            }
        }

        public T Find(string id)
        {
            return contents.Where(t => t.GetPrimaryKey() == id).FirstOrDefault();
        }

        public void Update(T t)
        {
            T toUpdate = Find(t.GetPrimaryKey());
            if (toUpdate != null)
            {
                contents[contents.IndexOf(toUpdate)] = t;
            }
        }

        public string Read() 
        {
            return contents.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();   
        
        public IEnumerator<T> GetEnumerator() => contents.GetEnumerator();
    }
}
