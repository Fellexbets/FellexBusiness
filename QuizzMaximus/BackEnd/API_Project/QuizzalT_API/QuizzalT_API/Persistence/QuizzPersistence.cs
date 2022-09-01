using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class QuizzPersistence : BasePersistence<Quizz>
    {
        public QuizzPersistence() => _contextEntity = _context.Quizzes;

        public async Task<Quizz> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Exists(int id) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id));
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
        public async Task<List<Quizz>> GetAllQuizzesUser(int id) => await _contextEntity.AsNoTracking().Where(e => e.UserId == id).ToListAsync();
        public async Task<List<Quizz>> GetAllQuizzesAdmin()
        {
            try
            {
                List<Quizz> quizzes = await _context.Quizzes.Where(q => q.UserId == 1).ToListAsync();

                return quizzes;
            }
            catch
            {
                return null;
            }
        }
    }
}
