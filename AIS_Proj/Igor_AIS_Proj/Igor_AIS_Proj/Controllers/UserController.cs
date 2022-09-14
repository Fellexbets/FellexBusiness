

using Microsoft.Extensions.Primitives;

namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;
        private readonly ISessionBusiness _sessionBusiness;

        public UserController(IUserBusiness userBusiness, ILogger<UserController> logger, ISessionBusiness sessionBusiness)
        {
            _userBusiness = userBusiness;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sessionBusiness = sessionBusiness;
        }


        [HttpGet("{id}")]
        public User GetById(int id) => _userBusiness.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await _userBusiness.Delete(id);

        [HttpGet]
        public List<User> GetAll() => _userBusiness.GetAll();

        [HttpPut]
        public Task<bool> Update(User user) => _userBusiness.Update(user);

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginUserResponse>> Authenticate(LoginUserRequest model)
        {
            try
            {
                User user = new User
                {
                    Email = model.Email,
                    UserPassword = model.UserPassword
                };
                var result = await _userBusiness.Authenticate(model);
                if (result.Item1)
                {
                    LoginUserResponse response = new LoginUserResponse
                    {
                        SessionId = result.Item4.SessionId.ToString(),
                        AccessToken = result.Item4.TokenAccess,
                        AccessTokenExpiresAt = result.Item4.TokenAccessExpireAt,
                        RefreshToken = result.Item4.RefreshToken,
                        RefreshTokenExpiresAt = result.Item4.Refresh_Token_expire_At,
                        User = new CreateUserResponse
                        {
                            UserId = result.Item3.UserId,
                            Username = result.Item3.Username,
                            Email = result.Item3.Email,
                            FullName = result.Item3.FullName,
                            CreatedAt = result.Item3.CreatedAt
                        }
                    };
                    return Ok(response);
                }
                return Unauthorized(result.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return Problem(ex.Message);
            }
        }


        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost, AllowAnonymous]

        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request)
        {
            var user = await _userBusiness.Create(request);
            if (user == null) { return BadRequest("User could not be created"); }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Logout()
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out StringValues authToken))
                    return BadRequest("No authorization found.");
                int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId == null)
                    return BadRequest("User Id not found.");
                Guid sessionId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                if (sessionId == null)
                    return BadRequest("Session Id not found.");
                var session = new Session
                {
                    SessionId = sessionId,
                    UserId = userId,
                    TokenAccess = authToken
                };
                var result = await _userBusiness.Logout(session);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginUserResponse>> RenewLogin(RenewLoginRequest tokenRefresh)
        {
            try
            {
                Guid sessionId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                var session = _sessionBusiness.GetById(sessionId);
                var resultSession = await _userBusiness.VerifySession(await session);
                if (!resultSession.Item1)
                    return Unauthorized(resultSession.Item2);
                var result = await _userBusiness.RenewLogin(resultSession.Item3, tokenRefresh.RefreshToken);
                if (result.Item1)
                {
                    LoginUserResponse response = new LoginUserResponse
                    {
                        AccessToken = result.Item4.TokenAccess,
                        AccessTokenExpiresAt = result.Item4.TokenAccessExpireAt,
                        RefreshToken = result.Item4.RefreshToken,
                        RefreshTokenExpiresAt = result.Item4.Refresh_Token_expire_At,
                        SessionId = result.Item4.SessionId.ToString(),
                        User = new CreateUserResponse
                        {
                            UserId = result.Item3.UserId,
                            Username = result.Item3.Username,
                            Email = result.Item3.Email,
                            FullName = result.Item3.FullName,
                            CreatedAt = result.Item3.CreatedAt
                        }
                    };
                    return Ok(response);
                }
                return Unauthorized(result.Item2);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return Problem(ex.Message);
            }



        }
    }
}
