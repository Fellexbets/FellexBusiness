using Igor_AIS_Proj.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using System.Linq;

namespace Igor_AIS_Proj.Persistence
{
    public class AccountPersistence : BasePersistence<Account>
    {
        public AccountPersistence() => _contextEntity = _context.Accounts;


        public Account GetById(int id) =>  _contextEntity.Include(a => a.Movements).FirstOrDefault(a => a.AccountId == id);

        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));


        public async Task<List<Account>> GetAllAccountsUser(int id) => await _contextEntity.AsNoTracking().Where(e => e.Userid == id).ToListAsync();

        public async Task<bool> TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount)
        {
            if (transferAmount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
                return false;
            }

            Account fromAccount = GetById(fromAccountId);
            Account toAccount = GetById(toAccountId);
            if (fromAccount.Currency != toAccount.Currency)
            {
                throw new ArgumentException("You can only transfer in the same currency");
                return false;
            }
            if (fromAccount.Balance < transferAmount)
            {
                throw new ApplicationException("insufficient funds");
                return false;
            }

            fromAccount.Balance -= transferAmount;
            toAccount.Balance += transferAmount;
            var transfer1 = new Transfer { Originaccountid = fromAccount.AccountId, Destinationaccountid = toAccount.AccountId, Amount = transferAmount, Currency = fromAccount.Currency };
            var mov1 = new Movement { Accountid = fromAccount.AccountId, Amount = -transferAmount, Currency = fromAccount.Currency };
            var mov2 = new Movement { Accountid = toAccount.AccountId, Amount = +transferAmount, Currency = toAccount.Currency };
            _context.Transfers.Add(transfer1);
            _context.Movements.Add(mov1);
            _context.Movements.Add(mov2);
            _context.SaveChangesAsync();
            return true;
        }



    }
}
