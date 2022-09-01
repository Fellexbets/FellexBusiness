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
    public class AnswerController : BaseController<AnswerBusiness, AnswerPersistence, Answer>
    {
        public AnswerController() : base() => business = new AnswerBusiness();


        [HttpGet("{id}")]
        public async Task<Answer> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpPost]
        public async Task<List<Answer>> CreateAnswers(List<Answer> answers) => await business.CreateAnswers(answers);

        [HttpPut]
        public async Task<bool> UpdateAnswers(List<Answer> answers) => await business.UpdateAnswers(answers);

        [HttpGet("{id}")]
        public async Task<List<Answer>> GetAnswersByUser(int id) => await business.GetAnswersByUser(id);

        [HttpGet]
        public async Task<List<Answer>> GetAllAnswersAdmin() => await business.GetAllAnswersAdmin();
    }
}

