using BlazorOpenBank.Data.Services;
using BlazorOpenBank.Data.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using BlazorOpenBank.Data.Models.APImodels;

namespace BlazorOpenBank.Data
{
    public class Refresh
    {
        public String? refreshToken { get; set; }

    }

    public class AccountService : IAccountService
    {
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _token = "token";
        private string _expires = "expires";
        private string _userid = "userid";
        private string _refreshToken = "refreshToken";
        private HttpClient _httpClient;
        public DateTimeOffset tokenDate { get; private set; }
        public string refreshT { get; set; }

        public User User { get; set; }

        public AccountService(
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            HttpClient httpClient)
        {
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task Initialize()
        {
            var token = await _localStorageService.GetItem<string>(_token);
        }

        public async Task<HttpResponseMessage> Login(LoginUserRequest model)
        {
            var httpRsp = await _httpClient.PostAsJsonAsync("/User/Authenticate", model);
            if (httpRsp.IsSuccessStatusCode)
            {
                var response = await httpRsp.Content.ReadFromJsonAsync(typeof(LoginUserResponse)) as LoginUserResponse;
                await _localStorageService.SetItem(_token, response.AccessToken);
                await _localStorageService.SetItem(_refreshToken, response.RefreshToken);
                await _localStorageService.SetItem(_expires, response.AccessTokenExpiresAt);
                await _localStorageService.SetItem(_userid, response.User.UserId);
                tokenDate = response.AccessTokenExpiresAt;
                User = new User
                {
                    Username = response.User.Username
                };
                return await ok(response);
            }
            return null;
        }
        public async Task RenewLogin()
        {
            AddBearerToken();
            var refreshToken = await _localStorageService.GetItem<String>(_refreshToken);
            var token = new RenewLoginRequest
            {
                RefreshToken = refreshToken
            };
            var result = await _httpClient.PostAsJsonAsync("/User/RenewLogin", token);
            if (result.IsSuccessStatusCode)
            {
                refreshT = refreshToken;
            }
            else
            {
                Console.WriteLine("yo");
            }
        }
        
        public async Task AddBearerToken()
        {
            string task1 = await _localStorageService.GetItem<string>(_token);
            if (task1 != null)
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", task1);
            // adicionar bool pra trabalhar depois nos metodos
        }

        public async Task<List<UploadResult>> GetAllUploads()
        {
            await AddBearerToken();
            List<UploadResult> model = null;
            var result = await _httpClient.GetAsync("/File/GetAllUploadsUser");
            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<UploadResult>>(jsonString);
                return model;
            }
            return null;
        }
        public async Task<List<Account?>> GetAllAccounts()
        {
            //var request = new RestRequest();
            //request.AddHeader("Authorization", "Bearer "+ _localStorageService.GetItem<string>("token"));
            await AddBearerToken();
            List<Account> model = null;
            var result = await _httpClient.GetAsync("/Account/GetAllAccountsUser");
            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<Account>>(jsonString);
                return model;
            }
            return null;
        }

        public async Task<AccountDetails> GetAccount(int accountId)
        {
            await AddBearerToken();
            var result = await _httpClient.GetAsync($"/Account/GetAccount/{accountId}");
            if (result.IsSuccessStatusCode)
            {
                
                //var jsonString = await result.Content.ReadAsStringAsync();
                var jsonString = await result.Content.ReadFromJsonAsync(typeof(AccountDetails)) as AccountDetails;
                //AccountDetails model = System.Text.Json.JsonSerializer.Deserialize<AccountDetails>(jsonString);
                //AccountDetails model = (AccountDetails)JsonConvert.DeserializeObject(jsonString, typeof(AccountDetails));
                //AccountDetails model = (AccountDetails)JsonConvert.DeserializeObject(jsonString);
                return jsonString;

            }
            else
            {
                return null;
            }
            
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            await AddBearerToken();
            var result = await _httpClient.PostAsJsonAsync("/Account/Create", request);
            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                CreateAccountResponse model = JsonConvert.DeserializeObject<CreateAccountResponse>(jsonString);
                return model;
            }
            return null; 
        }
        public async Task<bool> TransferFunds(TransferRequest request)
        {
            await AddBearerToken();
            var result = await _httpClient.PostAsJsonAsync("/Account/TransferFunds", request);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
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
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
            };
            return response;
        }
        public async Task Logout()
        {
            tokenDate = default;
            await _localStorageService.RemoveItem(_token);
            await _localStorageService.RemoveItem(_expires);
            await _localStorageService.RemoveItem(_userid);
            await _localStorageService.RemoveItem(_refreshToken);
            _navigationManager.NavigateTo("/login");
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
