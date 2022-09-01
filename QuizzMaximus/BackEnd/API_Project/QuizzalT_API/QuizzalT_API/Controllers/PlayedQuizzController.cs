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

namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class PlayedQuizzController : BaseController<PlayedQuizzBusiness, PlayedQuizzPersistence, PlayedQuizz>
    {
        public PlayedQuizzController() : base() => business = new PlayedQuizzBusiness();


        [HttpGet("{id}")]
        public async Task<PlayedQuizz> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


    }

}
