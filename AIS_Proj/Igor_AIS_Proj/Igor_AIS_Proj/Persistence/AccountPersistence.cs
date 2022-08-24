
namespace Igor_AIS_Proj.Persistence
{
    public class AccountPersistence : BasePersistence<Account>, IAccountPersistence
    {
        public AccountPersistence() => _contextEntity = _context.Accounts;


        public Account GetById(int id) =>  _contextEntity.Include(a => a.Movements).FirstOrDefault(a => a.AccountId == id);

        public List<Account> GetAllAccountsUser(int id) => _contextEntity.Where(e => e.UserId == id).Include(a => a.Movements).ToList();

        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));

        public async Task<CreateAccountResponse> Create(CreateAccountRequest request, int userId)
        {
            Account account = EntityMapper.MapRequestToAccountModel(request, userId);

            try
            {
                await _context.AddAsync(account);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return EntityMapper.MapAccountModelToContract(account);
        }

        
    }
}
