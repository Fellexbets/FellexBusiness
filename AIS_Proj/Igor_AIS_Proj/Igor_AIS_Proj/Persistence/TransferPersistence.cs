using Igor_AIS_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace Igor_AIS_Proj.Persistence
{
    public class TransferPersistence : BasePersistence<Transfer>
    {
        public TransferPersistence() => _contextEntity = _context.Transfers;

        public async Task<Transfer> GetById(int id1) => await _contextEntity.FindAsync(id1);
        public async Task<bool> Delete(int id1) => await Delete(_contextEntity.Find(id1));

        public void GetAllTransfersUser(int id) => _contextEntity.AsNoTracking().Where(e => e.OriginaccountId == id);
    }
}
