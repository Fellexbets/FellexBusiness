using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel;
using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizzalT_API.Models;

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

        [HttpPost, AllowAnonymous]
        public async Task<User> Authenticate(UserCredentials model) => await business.Authenticate(model);


    }
}
