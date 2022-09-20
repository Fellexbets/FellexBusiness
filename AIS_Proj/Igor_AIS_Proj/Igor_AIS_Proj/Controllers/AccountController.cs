
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<bool> Update(Account account) => _accountBusiness.Update(account);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        
        public async Task<ActionResult<CreateAccountResponse>> Create(CreateAccountRequest request)
        {
            if (!BoolUseridFromClaim(out int userId))
                return Unauthorized("Access not authorized.");

            try
            {
                (bool, CreateAccountResponse) account = await _accountBusiness.Create(request, userId);
                if (account.Item2 == null) { return BadRequest(); }
                return CreatedAtAction("GetAccount", new { accountId = account.Item2.AccountId }, account);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AccountMovims), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountMovims>> GetAccount(int accountId)
        {
            if (!BoolUseridFromClaim(out int userId))
                return Unauthorized("Access not authorized."); 
            var account =  _accountBusiness.GetById(accountId);
            var userIdClaim = GetUserIdFromClaim();
            if (account.Item2.UserId != userIdClaim)
                return Unauthorized("Access not authorized");
            try
            {
                (bool, AccountMovims?, string?) accountMovims = await _accountBusiness.GetAccount(accountId);
                if (accountMovims.Item2 == null) { return NoContent(); }
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
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Account>>> GetAllAccountsUser()
        {
            int userId = GetUserIdFromClaim();
            try
            {
                var result = await _accountBusiness.GetAllAccountsUser(userId);
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
            var userId = GetUserIdFromClaim();
            var accountTransfer = _accountBusiness.GetById(request.FromAccountId);
            if (accountTransfer.Item2 is null)
                return StatusCode(StatusCodes.Status400BadRequest);
            var userTransfer = _userBusiness.GetById(userId);
            if (accountTransfer.Item2.UserId != userTransfer.UserId)
                return StatusCode(StatusCodes.Status401Unauthorized);
            try
            {
                var sessionId = GetSessionIdFromClaim();
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
            
        private bool BoolUseridFromClaim(out int userId) =>
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId);

        private int GetUserIdFromClaim() => Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        private Guid GetSessionIdFromClaim() => Guid.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);


    }
}
