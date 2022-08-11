using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Controllers;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AccountController : BaseController<AccountBusiness, AccountPersistence, Account>
    {
        public AccountController() : base() => business = new AccountBusiness();


        [HttpGet("{id}")]
        public async Task<Account> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpGet("{id}")]
        public async Task<List<Account>> GetAllAccountsUser(int id)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                await business.GetAllAccountsUser(id);
            }
            return null;
        }

            
            

    }
}
