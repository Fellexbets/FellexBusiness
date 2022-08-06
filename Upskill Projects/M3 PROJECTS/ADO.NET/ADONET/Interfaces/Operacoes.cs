using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    
    public interface Operacoes<T> where T : Entity 
    {

        T CreateNewOfT();
        IQueryable<Entity> GetAll();

        Task<Entity> GetById(int id);

        Task Create(Entity entity);

        Task Update(int id, Entity entity);

        Task Delete(int id);

        Type TypeOfT();
    }

}
