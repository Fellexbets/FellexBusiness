using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using QuizzalT_API.Business;
using Microsoft.AspNetCore.Authorization;
using QuizzalT_API.Auxiliary;

namespace QuizzalT_API.Controllers
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
        public async Task<User> Authenticate(UserCredentials user) => await business.Authenticate(user);

        [HttpGet]
        public async Task<List<User>> GetAllDetailsUser(int id) => await business.GetAllDetailsUser(id);

        [HttpPost]
        public async Task<List<RelatedUser>> GetRelatedUsers(List<int> userIds) => await business.GetRelatedUsers(userIds);

        [HttpPut]
        public async Task<User> UpdateUser(User user) => await business.UpdateUser(user);
    }
}   
