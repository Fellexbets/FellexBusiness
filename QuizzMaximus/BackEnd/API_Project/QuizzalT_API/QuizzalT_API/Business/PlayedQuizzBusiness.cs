using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class PlayedQuizzBusiness : BaseBusiness<PlayedQuizzPersistence, PlayedQuizz>
    {
        public PlayedQuizzBusiness() => database = new PlayedQuizzPersistence();

        public async Task<PlayedQuizz> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

    }
}
