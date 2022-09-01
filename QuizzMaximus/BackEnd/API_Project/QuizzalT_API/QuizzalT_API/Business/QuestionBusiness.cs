using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class QuestionBusiness : BaseBusiness<QuestionPersistence, Question>
    {
        public QuestionBusiness() => database = new QuestionPersistence();

        public async Task<Question> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);
        public async Task<Question> UpdateQuestion(Question question) => await database.UpdateQuestion(question);
        public async Task<List<Question>> GetAllQuestionsUser(int id) => await database.GetAllQuestionsUser(id);
        public async Task<List<Question>> GetAllQuestionsAdmin() => await database.GetAllQuestionsAdmin();

    }
}
