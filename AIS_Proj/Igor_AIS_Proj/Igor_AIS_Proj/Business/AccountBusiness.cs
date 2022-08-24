

using System.Data.Entity;
using System.Transactions;

namespace Igor_AIS_Proj.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountPersistence _accountPersistence;
        private readonly IMovementPersistence _movementPersistence;
        private readonly ITransferPersistence _transferPersistence;
        public AccountBusiness(IAccountPersistence accountPersistence, IMovementPersistence movementPersistence, ITransferPersistence transferPersistence)
        {
            _transferPersistence = transferPersistence;
            _accountPersistence = accountPersistence;
            _movementPersistence = movementPersistence;
        }

            
        public Account GetById(int id) => _accountPersistence.GetById(id);
        public async Task<bool> Delete(int id) => await _accountPersistence.Delete(id);

        public async Task<bool> TransferFunds(TransferRequest request)
        {
            if (request.Amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
            }
            
            Account fromAccount = GetById(request.FromAccountId);
            Account toAccount = GetById(request.ToAccountId);
            if (fromAccount.Currency != toAccount.Currency)
            {
                throw new ArgumentException("You can only transfer in the same currency");
            }
            if (fromAccount.Balance < request.Amount)
            {
                throw new ApplicationException("insufficient funds");
            }

            try
            {
                using var context = new PostgresContext();
                using var transaction = context.Database.BeginTransaction();
                    fromAccount.Balance -= request.Amount;
                    toAccount.Balance += request.Amount;
                    var transfer1 = new Transfer { OriginaccountId = fromAccount.AccountId, DestinationaccountId = toAccount.AccountId, Amount = request.Amount, Currency = fromAccount.Currency };
                    var mov1 = new Movement { AccountId = fromAccount.AccountId, Amount = -request.Amount, Currency = fromAccount.Currency };
                    var mov2 = new Movement { AccountId = toAccount.AccountId, Amount = +request.Amount, Currency = toAccount.Currency };
                    await _transferPersistence.Create(transfer1);
                    await _movementPersistence.Create(mov1);
                    await _movementPersistence.Create(mov2);
                    await _accountPersistence.Update(fromAccount);
                    await _accountPersistence.Update(toAccount);
                    transaction.Commit();
                    return true;
                
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public async Task<List<Account>> GetAllAccountsUser(int id) => await _accountPersistence.GetAllAccountsUser(id);

        public  List<Account> GetAll() => _accountPersistence.GetAll();

        public async Task<CreateAccountResponse> Create(CreateAccountRequest request, int userId)
        {
            return await _accountPersistence.Create(request, userId);
        }
        public async Task<AccountMovims?> GetAccount(int accountId)
        {
            Account? account = GetById(accountId);
            if (account == null) { return null; }

            CreateAccountResponse contractsAccount = EntityMapper.MapAccountModelToContract(account);

            ICollection<Movim> movims = EntityMapper.MapMovementEnumerableToMovim(
                 _movementPersistence.GetAll().Where(movement => movement.AccountId == accountId));
            return new AccountMovims(contractsAccount, movims);
        }

        public  async Task<bool> Update(Account account) => await _accountPersistence.Update(account);

    }
}
