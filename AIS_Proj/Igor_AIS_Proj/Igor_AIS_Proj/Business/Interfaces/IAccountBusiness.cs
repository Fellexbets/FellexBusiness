
namespace Igor_AIS_Proj.Business.Interfaces
{
    public interface IAccountBusiness
    {
        Account GetById(int id);

        Task<bool> Delete(int id);
        Task<bool> TransferFunds(TransferRequest request);
        List<Account> GetAllAccountsUser(int id);
        List<Account> GetAll();
        Task<bool> Update(Account account);

        Task<AccountMovims?> GetAccount(int accountId);
        Task<CreateAccountResponse> Create(CreateAccountRequest accountRequest, int userId);



    }
}
