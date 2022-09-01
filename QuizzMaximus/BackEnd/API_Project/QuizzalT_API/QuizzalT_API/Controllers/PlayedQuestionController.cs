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
    public class PlayedQuestionController : BaseController<PlayedQuestionBusiness, PlayedQuestionPersistence, PlayedQuestion>
    {
        public PlayedQuestionController() : base() => business = new PlayedQuestionBusiness();


        [HttpGet("{id}")]
        public async Task<PlayedQuestion> GetById(int id) => await business.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await business.Delete(id);


        [HttpPost]
        public async Task<List<PlayedQuestion>> CreatePlayedQuestions(List<PlayedQuestion> playedQuestions) => await business.CreatePlayedQuestions(playedQuestions);

    }

}
