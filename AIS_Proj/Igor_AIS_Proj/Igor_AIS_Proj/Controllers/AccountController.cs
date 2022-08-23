
namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusiness _accountBusiness;

        public AccountController(IAccountBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
        }

        [HttpGet("{id}")]
        public Account GetById(int id) => _accountBusiness.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await _accountBusiness.Delete(id);

        [HttpGet]
        public List<Account> GetAll() => _accountBusiness.GetAll();

        [HttpPut]
        public Task<bool> Update(Account account) => _accountBusiness.Update(account);


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<Account> Create(Account account)
        {
            if (account.UserId == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                return await _accountBusiness.Create(account);
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are only authorized to create an account on your userpage!" };
            throw new System.Web.Http.HttpResponseException(msg);
            
        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<Account> GetAllAccountsUser(int id)
        {
            if (id == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                    return _accountBusiness.GetAllAccountsUser(id);
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You can only access information about your account!" };
            throw new System.Web.Http.HttpResponseException(msg);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> TransferFunds(TransferRequest request)
        {
            if (GetById(request.FromAccountId).UserId == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                await _accountBusiness.TransferFunds(request);
                return true;
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Transfer Failed! You can only make transfers from your account" };
            throw new System.Web.Http.HttpResponseException(msg);
        }   





    }
}
