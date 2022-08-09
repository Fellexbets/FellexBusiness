using Igor_AIS_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace Igor_AIS_Proj.Persistence
{
    public class TransferPersistence : BasePersistence<Transfer>
    {
        public TransferPersistence() => _contextEntity = _context.Transfers;

        public async Task<Transfer> GetById(int id1, int id2, int id3) => await _contextEntity.FindAsync(id1, id2, id3);
        public async Task<bool> Delete(int id1, int id2, int id3) => await Delete(_contextEntity.Find(id1, id2, id3));

        public void GetAllTransfersUser(int id) => _contextEntity.AsNoTracking().Where(e => e.Originaccountid == id);
    }
}
