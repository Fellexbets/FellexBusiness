
namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly ILogger<AccountController> _logger;
        private readonly ISessionBusiness _sessionBusiness;
        private readonly IUserBusiness _userBusiness;

        public AccountController(IAccountBusiness accountBusiness, ILogger<AccountController> logger, ISessionBusiness sessionBusiness, IUserBusiness userBusiness)
        {
            _accountBusiness = accountBusiness;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sessionBusiness = sessionBusiness;
            _userBusiness = userBusiness;
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
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            try
            {
                var result = await _accountBusiness.GetAllAccountsUser(id);
                return Ok(result);
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

        [ProducesResponseType(typeof(AccountMovims), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<bool>> TransferFunds(TransferRequest request)
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var accountTransfer = _accountBusiness.GetById(request.FromAccountId);
            if (accountTransfer is null)
                return StatusCode(StatusCodes.Status400BadRequest);
            var userTransfer = _userBusiness.GetById(userId);
            if (accountTransfer.UserId != userTransfer.UserId)
                return StatusCode(StatusCodes.Status401Unauthorized);
            try
            {
                var sessionId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                var newSession = await _sessionBusiness.GetById(sessionId);
                var session = await _sessionBusiness.ValidateSession(newSession);
                if (session.Item1 == false)
                    return StatusCode(StatusCodes.Status401Unauthorized, session.Item2);
                var done = await _accountBusiness.TransferFunds(request);
                return done.Item1 ? Ok(done) : BadRequest(done.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return Problem(ex.Message);
            }
            
        }
            
        private bool ValidateLoggedUser(out int userId) =>
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId);




    }
}
