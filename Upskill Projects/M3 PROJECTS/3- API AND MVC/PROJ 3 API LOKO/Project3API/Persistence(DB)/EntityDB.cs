using Microsoft.EntityFrameworkCore;
using Project3API.Models;

namespace Project3API.Persistence_DB_
{
    public abstract class EntityDB<T> where T : Entity
    {
        protected SQLiteDB db = new SQLiteDB();
        protected DbSet<T> dbEntity;

       
    }
}
