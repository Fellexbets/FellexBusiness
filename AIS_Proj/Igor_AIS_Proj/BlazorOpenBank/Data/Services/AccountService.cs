using BlazorOpenBank.Data.Services;
using BlazorOpenBank.Data.Services.Base;
using BlazorOpenBank.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text;
using System.Text.Json;
using CreateUserRequest = BlazorOpenBank.Data.Services.Base.CreateUserRequest;
using LoginUserRequest = BlazorOpenBank.Data.Services.Base.LoginUserRequest;

namespace BlazorOpenBank.Data
{
    
    public class AccountService : IAccountService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";
        private HttpClient _httpClient;

        public DateTimeOffset tokenDate { get; private set; }
        public User User { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
,
            HttpClient httpClient)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>(_userKey);
        }

        public async Task<HttpResponseMessage> Login(LoginUserRequest model)
        {
            var httpRsp = await _httpClient.PostAsJsonAsync("/User/Authenticate", model);
            if (httpRsp.IsSuccessStatusCode)
            {
                var response = await httpRsp.Content.ReadFromJsonAsync(typeof(LoginUserResponse)) as LoginUserResponse;
                await _localStorageService.SetItem(_userKey, response.AccessToken + response.AccessTokenExpiresAt);
                tokenDate = response.AccessTokenExpiresAt;
                return await ok(response);
            }
            return await error("Email or password is incorrect");
        }

        async Task<HttpResponseMessage> ok(object body = null)
        {
            return await jsonResponse(HttpStatusCode.OK, body ?? new { });
        }
        async Task<HttpResponseMessage> error(string message)
        {
            return await jsonResponse(HttpStatusCode.BadRequest, new { message });
        }
        async Task<HttpResponseMessage> jsonResponse(HttpStatusCode statusCode, object content)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
            };
            return response;
        }
        public async Task Logout()
        {
            tokenDate = default;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("/login");
        }

        public async Task<IList<User>> GetAll()
        {
            return await _httpService.Get<IList<User>>("/users");
        }

        public async Task<User> GetById(string id)
        {
            return await _httpService.Get<User>($"/users/{id}");
        }

        //    public class UserService
        //{
        //    private HttpClient _httpClient;
        //    public UserService(HttpClient httpClient)
        //    {
        //        _httpClient = httpClient;
        //    }

        //    public async Task Register(CreateUserRequest request)
        //    {
        //        await _httpClient.PostAsJsonAsync("/User/Create", request); 
        //    }
        //    public async Task Authenticate(LoginUserRequest request)
        //    {
        //        await _httpClient.PostAsJsonAsync("/User/Authenticate", request);
        //    }
        //}
    }
}
