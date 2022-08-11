using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel;
using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class UserController : BaseController<UserBusiness, UserPersistence, User>
    {
       

        public UserController() : base() => business = new UserBusiness();

       

        [HttpGet("{id}")]
        public async Task<User> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await business.RegisterAsync(request.Email, request.Userpassword);
            return Ok();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        //{
        //    
        //}


        //[HttpPost]
        //public User Register(User user) => business.Register(user);




    }
}
