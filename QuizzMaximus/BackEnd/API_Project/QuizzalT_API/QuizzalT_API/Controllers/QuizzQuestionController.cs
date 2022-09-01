using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using QuizzalT_API.Business;

namespace QuizzalT_API.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class QuizzQuestionController : BaseController<QuizzQuestionBusiness, QuizzQuestionPersistence, QuizzQuestion>
    {
        public QuizzQuestionController() : base() => business = new QuizzQuestionBusiness();


        [HttpGet("{id1},{id2}")]
        public async Task<QuizzQuestion> GetById(int id1, int id2) => await business.GetById(id1, id2);

        [HttpDelete("{id1},{id2}")]
        public async Task<bool> Delete(int id1, int id2) => await business.Delete(id1, id2);

        [HttpPost]
        public async Task<List<QuizzQuestion>> CreateQuizzQuestions(List<QuizzQuestion> quizzQuestions) => await business.CreateQuizzQuestions(quizzQuestions);

        [HttpGet("{id}")]
        public async Task<List<QuizzQuestion>> GetAllQuizzQuestionsUser(int id) => await business.GetAllQuizzQuestionsUser(id);

        [HttpGet]
        public async Task<List<QuizzQuestion>> GetAllQuizzQuestionsAdmin() => await business.GetAllQuizzQuestionsAdmin();
    }
}