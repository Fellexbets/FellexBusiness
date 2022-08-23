
namespace Igor_AIS_Proj.Persistence
{
    public class AccountPersistence : BasePersistence<Account>, IAccountPersistence
    {
        public AccountPersistence() => _contextEntity = _context.Accounts;


        public Account GetById(int id) =>  _contextEntity.Include(a => a.Movements).FirstOrDefault(a => a.AccountId == id);

        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));

    }
}
