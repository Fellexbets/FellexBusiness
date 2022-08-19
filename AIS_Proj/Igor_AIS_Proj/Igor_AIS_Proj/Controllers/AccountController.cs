using System.Collections.Generic;
using System.Net;
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
        

        public AccountController() : base()
        {
            business = new AccountBusiness();
            
        }


        [HttpGet("{id}")]
        public Account GetById(int id) => business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<Account> GetAllAccountsUser(int id)
        {
            if (id == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                    return  business.GetAllAccountsUser(id);
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You can only access information about your account!" };
            throw new System.Web.Http.HttpResponseException(msg);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> TransferFunds(TransferRequest request)
        {
            if (GetById(request.FromAccountId).UserId == Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                await business.TransferFunds(request);
                return true;
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Transfer Failed! You can only make transfers from your account" };
            throw new System.Web.Http.HttpResponseException(msg);
        }   





    }
}
