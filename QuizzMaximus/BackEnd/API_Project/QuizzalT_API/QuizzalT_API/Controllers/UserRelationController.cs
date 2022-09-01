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
    public class UserRelationController : BaseController<UserRelationBusiness, UserRelationPersistence, UserRelation>
    {
        public UserRelationController() : base() => business = new UserRelationBusiness();


        [HttpGet("{id1},{id2}")]
        public async Task<UserRelation> GetById(int id1, int id2) => await business.GetById(id1, id2);

        [HttpDelete("{id1},{id2}")]
        public async Task<bool> Delete(int id1, int id2) => await business.Delete(id1, id2);


        [HttpGet("{id}")]
        public async Task<List<UserRelation>> GetAllRelationsUser(int id) => await business.GetAllRelationsUser(id);

    }
}   
