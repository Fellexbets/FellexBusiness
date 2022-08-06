using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project3API.Persistence_DB_
{
    public abstract class BaseDB<T> where T : Entity
    {
        protected SQLiteDB db = new SQLiteDB();
        protected DbSet<T> dbEntity;

        public T Create(T entity)
        {
            db.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public List<T> GetAll()
        {
            return dbEntity.ToList();
        }

        [HttpGet("{id}")]
        public T GetById(int id)
        {
            return dbEntity.Find(id);
        }

        public void Update(T entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }

        public void Delete (int id)
        {
            T entity = GetById(id);
            db.Remove(entity);
            db.SaveChanges();
        }
        
    }
}
