using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Igor_AIS_Proj.Business
{
    public abstract class BaseBusiness<U, T> where U : BasePersistence<T> where T : Entity
    {
        protected U database;
        public virtual List<T> GetAll() =>  database.GetAll();

        public virtual async Task<T> Create(T entity) => await database.Create(entity);
        public virtual async Task<bool> Update(T entity) => await database.Update(entity);

        public virtual async Task<bool> Delete(T entity) => await database.Delete(entity);
    }
}
