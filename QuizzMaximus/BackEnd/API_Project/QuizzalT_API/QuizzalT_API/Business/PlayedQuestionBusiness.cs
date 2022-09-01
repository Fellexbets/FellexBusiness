using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class PlayedQuestionBusiness : BaseBusiness<PlayedQuestionPersistence, PlayedQuestion>
    {
        public PlayedQuestionBusiness() => database = new PlayedQuestionPersistence();

        public async Task<PlayedQuestion> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

        public async Task<List<PlayedQuestion>> CreatePlayedQuestions(List<PlayedQuestion> playedQuestions) => await database.CreatePlayedQuestions(playedQuestions);

    }
}
