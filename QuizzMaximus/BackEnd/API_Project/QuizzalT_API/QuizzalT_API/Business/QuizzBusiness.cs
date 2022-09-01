using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class QuizzBusiness : BaseBusiness<QuizzPersistence, Quizz>
    {
        public QuizzBusiness() => database = new QuizzPersistence();

        public async Task<Quizz> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

        public async Task<List<Quizz>> GetAllQuizzesUser(int id) => await database.GetAllQuizzesUser(id);

        public async Task<List<Quizz>> GetAllQuizzesAdmin() => await database.GetAllQuizzesAdmin();
    }
}
