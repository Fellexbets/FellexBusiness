using System.Collections.Generic;
using System.Threading.Tasks;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Controllers;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using QuizzalT_API.Business;

namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AccountController : BaseController<AccountBusiness, AccountPersistence, Account>
    {
        public AccountController() : base() => business = new AccountBusiness();


        [HttpGet("{id1}")]
        public async Task<Account> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id1}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpGet("{id}")]
        public async Task<List<Account>> GetAllAccountsUser(int id) => await business.GetAllAccountsUser(id);

    }
}
