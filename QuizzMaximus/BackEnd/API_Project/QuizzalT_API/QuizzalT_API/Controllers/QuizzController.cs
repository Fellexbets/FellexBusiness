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
    public class QuizzController : BaseController<QuizzBusiness, QuizzPersistence, Quizz>
    {
        public QuizzController() : base() => business = new QuizzBusiness();


        [HttpGet("{id}")]
        public async Task<Quizz> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpGet("{id}")]
        public async Task<List<Quizz>> GetAllQuizzesUser(int id) => await business.GetAllQuizzesUser(id);

        [HttpGet]
        public async Task<List<Quizz>> GetAllQuizzesAdmin() => await business.GetAllQuizzesAdmin();

    }  
}
