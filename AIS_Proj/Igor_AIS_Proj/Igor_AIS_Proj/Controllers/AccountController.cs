
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
            if (!ValidateLoggedUser(out int userId)) { return Unauthorized("Access not authorized."); }

            try
            {
                CreateAccountResponse? account = await _accountBusiness.Create(request, userId);
                if (account == null) { return BadRequest(); }
                await _accountBusiness.Create(request, userId);
                return CreatedAtAction("GetAccount", new { accountId = account.AccountId }, account);
            }
            catch(Exception ex)
            {
                switch(ex)
                {
                    case ArgumentException: return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                    default: return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            
        }
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AccountMovims), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountMovims>> GetAccount(int accountId)
        {
            if (!ValidateLoggedUser(out int userId))
            { return Unauthorized("Access not authorized."); }
            
            try
            {
                AccountMovims? accountMovims = await _accountBusiness.GetAccount(accountId);
                if (accountMovims == null) { return NoContent(); }
                return Ok(accountMovims);
            }
            catch (Exception ex)
            {
                    switch(ex)
                    {
                        case ArgumentException: return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                        default: return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                    }
            }
            
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Account>>> GetAllAccountsUser(int id)
        {
            if (id != Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You can only access information about your account!" };
                throw new System.Web.Http.HttpResponseException(msg);
            }
            try
            {
                return await _accountBusiness.GetAllAccountsUser(id);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: return  StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                    default: return  StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> TransferFunds(TransferRequest request)
        {
            if (!ValidateLoggedUser(out int userId))
                throw new ArgumentException("You can only transfer in the same currency");
            await _accountBusiness.TransferFunds(request);
            return true;   
        }
            
        private bool ValidateLoggedUser(out int userId) =>
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value, out userId);




    }
}
