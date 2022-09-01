using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class AchievementBusiness : BaseBusiness<AchievementPersistence, Achievement>
    {
        public AchievementBusiness() => database = new AchievementPersistence();


        public async Task<Achievement> GetById(int id1, int id2) => await database.GetById(id1, id2);
        public async Task<bool> Delete(int id1, int id2) => await database.Delete(id1, id2);

        public async Task<List<Achievement>> GetAllAchievementsUser(int id) => await database.GetAllAchievementsUser(id);

    }
}
