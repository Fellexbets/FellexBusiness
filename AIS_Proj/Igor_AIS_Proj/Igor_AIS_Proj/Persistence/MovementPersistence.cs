using Igor_AIS_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace Igor_AIS_Proj.Persistence
{
    public class MovementPersistence : BasePersistence<Movement>
    {
        public MovementPersistence() => _contextEntity = _context.Movements;

        public async Task<Movement> GetById(int id1) => await _contextEntity.FindAsync(id1);
        public async Task<bool> Delete(int id1) => await Delete(_contextEntity.Find(id1));

        //public void GetAllMovementsUser(int id) => _contextEntity.AsNoTracking().Where(e => e.Originaccountid == id);
    }
}
