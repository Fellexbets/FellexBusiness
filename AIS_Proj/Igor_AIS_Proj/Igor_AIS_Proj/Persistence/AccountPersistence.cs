using Igor_AIS_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace Igor_AIS_Proj.Persistence
{
    public class AccountPersistence : BasePersistence<Account>
    {
        public AccountPersistence() => _contextEntity = _context.Accounts;

        public async Task<Account> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
        public async Task<List<Account>> GetAllAccountsUser(int id) => await _contextEntity.AsNoTracking().Where(e => e.Userid == id).ToListAsync();
    }
}
