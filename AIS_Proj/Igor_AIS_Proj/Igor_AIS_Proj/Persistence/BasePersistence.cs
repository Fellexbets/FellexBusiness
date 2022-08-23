
namespace Igor_AIS_Proj.Persistence
{
    public class BasePersistence<T> where T : Entity
    {

        protected PostgresContext _context = new PostgresContext();
        protected DbSet<T> _contextEntity;

        public virtual List<T> GetAll() =>  _contextEntity!.AsNoTracking().ToList();
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
