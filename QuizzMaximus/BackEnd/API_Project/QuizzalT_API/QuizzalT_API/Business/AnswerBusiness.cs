using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class AnswerBusiness : BaseBusiness<AnswerPersistence, Answer>
    {
        public AnswerBusiness() => database = new AnswerPersistence();

        public async Task<Answer> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

        public async Task<List<Answer>> CreateAnswers(List<Answer> answers) => await database.CreateAnswers(answers);
        public async Task<bool> UpdateAnswers(List<Answer> answers) => await database.UpdateAnswers(answers);
        public async Task<List<Answer>> GetAnswersByUser(int userId) => await database.GetAnswersByUser(userId);
        public async Task<List<Answer>> GetAllAnswersAdmin() => await database.GetAllAnswersAdmin();
    }
}
