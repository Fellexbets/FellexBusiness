using BlazorOpenBank2.Data.Models;



namespace BlazorOpenBank2.Data
{
    public interface IAccountService
    {
        User User { get; }
        DateTimeOffset tokenDate { get; }

        string refreshT { get; set; }
        Task Initialize();
        Task<HttpResponseMessage> Login(LoginUserRequest model);
        Task Logout();
        Task<bool> TransferFunds(TransferRequest model);
        Task<List<Movim>> GetAccount(int accountId);
        Task<List<Account>> GetAllAccounts();
        Task RenewLogin();
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
    }
}
