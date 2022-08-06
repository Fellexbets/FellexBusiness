using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    // this interface is applied to the generic models that are a part of T and have Ihasprimarykey implemented.
    // this is another way of safeguarding it!
    // CRUD means create add read update, i think
    // all the other methods necessary are added here, ofc, easly applied to all models through this interface.
    public interface Operacoes<Entity> where Entity: class
    {
        
        IQueryable<Entity> GetAll();

        Task<Entity> GetById(int id);

        Task Create(Entity entity);

        Task Update(int id, Entity entity);

        Task Delete(int id);
    }

}
