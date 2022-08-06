using Microsoft.AspNetCore.Mvc;
using Project3API.Models;
using Project3API.Persistence_DB_;
using System.Collections.Generic;

namespace Project3API.Business_Logica_aplicacional_
{
	public abstract class BaseBusiness<T, U> where T : BaseDB<U> where U : Entity
	{
		protected T database;

        public List<U> GetAll()
        {
            return database.GetAll();
        }
        [HttpGet("{id}")]

        public U GetById(int id)
        {
            return database.GetById(id);
        }

        public void Update(U entity)
        {
            database.Update(entity);
           
        }
        public void Create(U Entity)
        {
            database.Create(Entity);

        }
        public void Remove(int id)
        {
             database.Delete(id);
            
        }
    }
}