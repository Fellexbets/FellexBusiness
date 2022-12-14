using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public abstract class BaseBusiness<U, T> where U : BasePersistence<T> where T : Entity
    {
        protected U database;
        public virtual async Task<List<T>> GetAll() => await database.GetAll();

        public virtual async Task<T> Create(T entity) => await database.Create(entity);
        public virtual async Task<bool> Update(T entity) => await database.Update(entity);

        public virtual async Task<bool> Delete(T entity) => await database.Delete(entity);
    }
}
