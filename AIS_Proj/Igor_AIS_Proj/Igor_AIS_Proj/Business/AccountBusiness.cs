using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Igor_AIS_Proj.Business
{
  
    public class AccountBusiness : BaseBusiness<AccountPersistence, Account>
    {
        public AccountBusiness()
        {
            database = new AccountPersistence();
            _movementPersistence = new MovementPersistence();
            _transferPersistence = new TransferPersistence();
        }

        private MovementPersistence _movementPersistence;
        private TransferPersistence _transferPersistence;
            
        public Account GetById(int id) =>  database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

        public async Task<bool> TransferFunds(TransferRequest request)
        {
            if (request.Amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
                return false;
            }

            Account fromAccount = GetById(request.FromAccountId);
            Account toAccount = GetById(request.ToAccountId);
            if (fromAccount.Currency != toAccount.Currency)
            {
                throw new ArgumentException("You can only transfer in the same currency");
                return false;
            }
            if (fromAccount.Balance < request.Amount)
            {
                throw new ApplicationException("insufficient funds");
                return false;
            }

            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;
            var transfer1 = new Transfer { OriginaccountId = fromAccount.AccountId, DestinationaccountId = toAccount.AccountId, Amount = request.Amount, Currency = fromAccount.Currency };
            var mov1 = new Movement { AccountId = fromAccount.AccountId, Amount = -request.Amount, Currency = fromAccount.Currency };
            var mov2 = new Movement { AccountId = toAccount.AccountId, Amount = +request.Amount, Currency = toAccount.Currency };
            _transferPersistence.Create(transfer1);
            _movementPersistence.Create(mov1);
            _movementPersistence.Create(mov2);
            return true;
        }

       

        public List<Account> GetAllAccountsUser(int id) => database.GetAll().Where(e => e.UserId == id).ToList();

    }
}
