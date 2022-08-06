using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    // this interface is applied to the generic models that are a part of T and have Ihasprimarykey implemented.
    // this is another way of safeguarding it!
    // CRUD means create add read update, i think
    // all the other methods necessary are added here, ofc, easly applied to all models through this interface.
    public interface ICrud<T> where T : Entity, IHasPrimaryKey
    {
        T CreateNewOfT();
        Type TypeOfT();
        void Add(T obj);
        string Read();
        void Remove(string id);

        T Find(string id);

        void Update(T obj);

        void SaveChanges();
    }
}
