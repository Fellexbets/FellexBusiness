using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Igor_AIS_Proj.Business
{
    public class AccountBusiness : BaseBusiness<AccountPersistence, Account>
    {
        public AccountBusiness() => database = new AccountPersistence();


        public async Task<Account> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

        public async Task<List<Account>> GetAllAccountsUser(int id) => await database.GetAllAccountsUser(id);

    }
}
