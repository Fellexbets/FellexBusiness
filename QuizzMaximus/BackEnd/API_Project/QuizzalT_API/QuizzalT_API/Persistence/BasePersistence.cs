using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class BasePersistence<T> where T : Entity
    {

        protected QuizzalTContext _context = new QuizzalTContext();
        protected DbSet<T> _contextEntity;

        public virtual async Task<List<T>> GetAll() => await _contextEntity.AsNoTracking().ToListAsync();
        public virtual async Task<T> Create(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                return entity;
            }
        }
        public virtual async Task<bool> Update(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> Delete(T entity)
        {
            if (entity == null) { return false; }
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
