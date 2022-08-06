﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Text.Encodings.Web;
using System.Reflection;

namespace ClassLibrary1
{
    public class Manager<T> : IEnumerable<T> where T : IHasPrimaryKey
    {
        private List<T> contents;

        public static Manager<T> Instance { get; private set; }
        static Manager() => Instance = new Manager<T>();
        private Manager() => GenerateList();


        private void GenerateList()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            if (!File.Exists(pathAttribute.Path))
            {
                contents = new List<T>();
                return;
            }
            string jsonString = File.ReadAllText(pathAttribute.Path);
            contents = JsonSerializer.Deserialize<List<T>>(jsonString);
        }

        public void AdicionarIten(T t)
        {
            contents.Add(t);
        }

       

        public void SaveChanges()
        {
            PathAttribute pathAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(PathAttribute)) as PathAttribute;
            string path = pathAttribute.Path;
            string contentsString = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, contentsString);
        }

        

        public void RemoverTudo(string ID)
        {
            T toRemove = Find(ID);
            if (toRemove != null)
            {
                contents.Remove(toRemove);
            }
        }

        public T Find(string id)
        {
            return contents.Where(t => t.GetPrimaryKey() == id).FirstOrDefault();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return contents.GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return contents.GetEnumerator();
        }





    }











}

