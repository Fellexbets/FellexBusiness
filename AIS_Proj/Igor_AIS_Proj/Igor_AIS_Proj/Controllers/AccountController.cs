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
        public Account GetById(int id) => business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpGet("{id}")]
        [Authorize]
        public async Task<List<Account>> GetAllAccountsUser(int id)
        {
            try
            {
                //if (id == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                    return await business.GetAllAccountsUser(id);
                //return null;
            }   
            catch
            {
                return null;
            }
                
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount)
        {
            await business.TransferFunds(fromAccountId, toAccountId, transferAmount);
            return true;
        }





    }
}
