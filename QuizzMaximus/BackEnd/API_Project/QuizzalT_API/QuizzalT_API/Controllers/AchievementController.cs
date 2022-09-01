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
    public class AchievementController : BaseController<AchievementBusiness, AchievementPersistence, Achievement>
    {
        public AchievementController() : base() => business = new AchievementBusiness();


        [HttpGet("{id1},{id2}")]
        public async Task<Achievement> GetById(int id1, int id2) => await business.GetById(id1, id2);

        [HttpDelete("{id1},{id2}")]
        public async Task<bool> Delete(int id1, int id2) => await business.Delete(id1, id2);


        [HttpGet("{id}")]
        public async Task<List<Achievement>> GetAllAchievementsUser(int id) => await business.GetAllAchievementsUser(id);

    }
}
