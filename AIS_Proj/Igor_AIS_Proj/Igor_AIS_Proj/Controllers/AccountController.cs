
namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountBusiness accountBusiness, ILogger<AccountController> logger)
        {
            _accountBusiness = accountBusiness;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public Account GetById(int id) => _accountBusiness.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await _accountBusiness.Delete(id);

        [HttpGet]
        public List<Account> GetAll() => _accountBusiness.GetAll();

        [HttpPut]
        public Task<bool> Update(Account account) => _accountBusiness.Update(account);

        [ProducesResponseType(typeof(CreateAccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<CreateAccountResponse>> Create(CreateAccountRequest request)
        {
            if (!ValidateLoggedUser(out int userId)) { return Unauthorized("Invalid access."); }
            CreateAccountResponse? account = await _accountBusiness.Create(request, userId);
            if (account == null) { return BadRequest(); }
            return await _accountBusiness.Create(request, userId);
            return CreatedAtAction("GetAccount", new { accountId = account.AccountId }, account);
        }
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AccountMovims), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountMovims>> GetAccount(int accountId)
        {
            if (!ValidateLoggedUser(out int userId))
            { return Unauthorized("Invalid access."); }

            AccountMovims? accountMovims = await _accountBusiness.GetAccount(accountId);
            if (accountMovims == null) { return NoContent(); }
            return Ok(accountMovims);
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
        public async Task<ActionResult<bool>> TransferFunds(TransferRequest request)
        {
            if (GetById(request.FromAccountId).UserId == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                await _accountBusiness.TransferFunds(request);
                return true;
            return Unauthorized("Invalid access.");
        }
            
        private bool ValidateLoggedUser(out int userId) =>
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out userId);




    }
}
