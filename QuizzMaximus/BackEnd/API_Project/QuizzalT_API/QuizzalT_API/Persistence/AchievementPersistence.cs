using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class AchievementPersistence : BasePersistence<Achievement>
    {
        public AchievementPersistence() => _contextEntity = _context.Achievements;

        public async Task<Achievement> GetById(int id1, int id2) => await _contextEntity.FindAsync(id1, id2);
        public async Task<bool> Exists(int id1, int id2) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id1) && e.ReturnId().Contains(id2));
        public async Task<bool> Delete(int id1, int id2) => await Delete(_contextEntity.Find(id1, id2));
        public async Task<List<Achievement>> GetAllAchievementsUser(int id) => await _contextEntity.AsNoTracking().Where(e => e.UserId == id).ToListAsync();
    }
}
