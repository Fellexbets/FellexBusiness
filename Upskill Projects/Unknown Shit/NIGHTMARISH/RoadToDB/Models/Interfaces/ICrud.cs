using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    public interface ICrud<T> where T : Entity, IHasPrimaryKey
    {
        T CreateNewOfT();
        Type TypeOfT();
        void Add(T obj);
        
        void Remove(string id);

        T Find(string id);

        void Update(T obj);

        void SaveChanges();
    }
}
