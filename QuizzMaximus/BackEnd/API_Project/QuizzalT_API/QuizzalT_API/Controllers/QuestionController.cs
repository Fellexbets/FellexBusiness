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
    public class QuestionController : BaseController<QuestionBusiness, QuestionPersistence, Question>
    {
        public QuestionController() : base() => business = new QuestionBusiness();


        [HttpGet("{id}")]
        public async Task<Question> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);

        [HttpPut]
        public async Task<Question> UpdateQuestion(Question question) => await business.UpdateQuestion(question);

        [HttpGet("{id}")]
        public async Task<List<Question>> GetAllQuestionsUser(int id) => await business.GetAllQuestionsUser(id);

        [HttpGet]
        public async Task<List<Question>> GetAllQuestionsAdmin() => await business.GetAllQuestionsAdmin();

    }
}
