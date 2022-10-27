using BlazorOpenBank.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorOpenBank.Data
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
        Task<AccountDetails> GetAccount(int accountId);
        Task<List<Account>> GetAllAccounts();
        Task RenewLogin();
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
    }
}
