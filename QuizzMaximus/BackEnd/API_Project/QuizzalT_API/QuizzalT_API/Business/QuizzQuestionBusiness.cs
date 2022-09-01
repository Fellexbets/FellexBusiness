using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class QuizzQuestionBusiness : BaseBusiness<QuizzQuestionPersistence, QuizzQuestion>
    {
        public QuizzQuestionBusiness() => database = new QuizzQuestionPersistence();

        public async Task<QuizzQuestion> GetById(int id1, int id2) => await database.GetById(id1, id2);
        public async Task<bool> Delete(int id1, int id2) => await database.Delete(id1, id2);

        public async Task<List<QuizzQuestion>> CreateQuizzQuestions(List<QuizzQuestion> quizzQuestions) => await database.CreateQuizzQuestions(quizzQuestions);
        public async Task<List<QuizzQuestion>> GetAllQuizzQuestionsUser(int id) => await database.GetAllQuizzQuestionsUser(id);
        public async Task<List<QuizzQuestion>> GetAllQuizzQuestionsAdmin() => await database.GetAllQuizzQuestionsAdmin();
    }
}