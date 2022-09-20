
namespace Igor_AIS_Proj.Business.Interfaces
{
    public interface IAccountBusiness
    {
        (bool, Account) GetById(int id);

        Task<bool> Delete(int id);
        Task<(bool, string?)> TransferFunds(TransferRequest request);
        Task<(bool, List<Account>)> GetAllAccountsUser(int id);
        List<Account> GetAll();
        Task<bool> Update(Account account);

        Task<(bool, AccountMovims?, string?)> GetAccount(int accountId);
        Task<(bool, CreateAccountResponse)> Create(CreateAccountRequest request, int userId);

        IEnumerable<Account> VerifyTransfer(TransferRequest request, int userId);



    }
}
