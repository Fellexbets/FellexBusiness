using BlazorOpenBank.Data.Services;
using BlazorOpenBank.Data.Services.Base;
using BlazorOpenBank.Models;
using Microsoft.AspNetCore.Components;
using CreateUserRequest = BlazorOpenBank.Data.Services.Base.CreateUserRequest;
using LoginUserRequest = BlazorOpenBank.Data.Services.Base.LoginUserRequest;

namespace BlazorOpenBank.Data
{
    public interface IAccountService
    {
        User User { get; }

        DateTimeOffset tokenDate { get; }
        Task Initialize();
        Task<HttpResponseMessage> Login(LoginUserRequest model);
        Task Logout();
        Task<IList<User>> GetAll();
        Task<User> GetById(string id);
    }
}
