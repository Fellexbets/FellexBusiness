﻿

namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBusiness userBusiness, ILogger<UserController> logger)
        {
            _userBusiness = userBusiness;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet("{id}")]
        public async Task<User> GetById(int id) => await _userBusiness.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await _userBusiness.Delete(id);

        [HttpGet]
        public List<User> GetAll() => _userBusiness.GetAll();

        [HttpPut]
        public Task<bool> Update(User user) => _userBusiness.Update(user);

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginUserRequest model)
        {
            return Ok(_userBusiness.Authenticate(model));
        }


        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request)
        {
            var user = await _userBusiness.Create(request);
            if(user == null) { return BadRequest("User could not be created"); }
            return Ok(user);
        }

    }
}
