using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace RoadToDB
{
    // Representa um conjunto de entidades
    // the manager is used to generically instantiate all models, it applys the Icrud interface (that has the methods) and
    // this is where we should apply the IEnumerable (allowing the foreach to work). Also worth mentioning the safeguard again: the T instantiated has to be a part of entity, has to have 
    // IHasPrimaryKey applied and the new() restriction forces the generic class to have a public constructor without parameters.
    public class Manager<T> : ICrud<Entity>, IEnumerable<T> where T : Entity, IHasPrimaryKey, new()
    {

        // IConfigurationRoot is an interface from the system (we do not need to create it, we just need to be using Microsoft.Extensions.Configuration)
        // it is applied like this because it is used in more then one place: in load from file and in the manager's constructor. In this way, we can just call the Iconfigurationroot variable "configurationroot" anywhere we would like.
        public static IConfigurationRoot ConfigurationRoot { get; private set; }

        
        private List<T> contents;

        public static Manager<T> Instance { get; private set; }

        static Manager()
        {
            ConfigurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            Instance = new Manager<T>();
        }

        private Manager() => LoadFromFile();

        // this method loads the predetermined appsettings.json applying the IConfigurationRoot variable ConfigurationRoot["JsonFilesPath"] to the name of the path determinated in appsettings.json
        // if the path is wrong, we create a new blank list.
        // if the path is correct, we first read the text from the path and associate it to a string and then we deserialize the string and assign them to the list
        private void LoadFromFile()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            string path = ConfigurationRoot["JsonFilesPath"] + pathAttribute.Path;
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
            bool firstOne = true;
            foreach (T t in contents)
            {
                if (firstOne) { lines.Add(t.Header()); firstOne = false; }
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

        public string Read()
        {
            return ToString();
        }

        public void Remove(string id)
        {
            T toRemove = contents.Where(t => t.GetPrimaryKey() == id).FirstOrDefault();
            if (toRemove != null) 
            { 
                contents.Remove(toRemove); 
            }
        }
        // the Find function also uses the "where" filter, this "t" parameter could be anything really
        // the important part is the getprimarykey method is equal to the id parameter used in the find function.
        public T Find(string id)
        {
            return contents.Where(t => t.GetPrimaryKey() == id).FirstOrDefault();   
        }

        public void Update(T obj)
            // here we assign the local variable with the T type the contents list but filtered, we only want to update one of the entrys, the one where 
            // the primary key is the primary key given as a parameter to the function. It is important to use firstordefault to ensure that if it does not
            // find anything, it uses the one that was there already.
            // It is interesting that we use an object type and not string or integer, this happens, imo, because there are some string primarykeys, 
            // if there only were integer primarykeys, we could just use an integer as a parameter! i guess
        {
            T toUpdate = contents.Where(toUpdate => toUpdate.GetPrimaryKey() == obj.GetPrimaryKey()).FirstOrDefault();
            if (toUpdate != null)
            {
                contents[contents.IndexOf(toUpdate)] = obj;
            }
        }

        // to save the changes we defined a local variable inside the class PathAttribute and, using reflection,
        // we get the attribute applied
        public void SaveChanges() 
        {
            // i do not fully understand this
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            // this one bellow makes sense to me: the string path is assigned a configurationroot path and is a part of pathAttribute class now
            string path = ConfigurationRoot["JsonFilesPath"] + pathAttribute.Path;
            // the string contentstring is where we serialize the contents list (where we first deserialized the json from)
            string contentsString = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true });
            // and here we overrite the path with the new serialized string contentsstring.
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
