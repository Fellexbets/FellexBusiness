﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizzalT_API.Business;

namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class UserController : BaseController<UserBusiness, UserPersistence, User>
    {
        public UserController() : base() => business = new UserBusiness();


        [HttpGet("{id}")]
        public async Task<User> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id1}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


    }
}
