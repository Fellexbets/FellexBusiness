using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class ThemePersistence : BasePersistence<Theme>
    {
        public ThemePersistence() => _contextEntity = _context.Themes;

        public async Task<Theme> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Exists(int id) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id));
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
    }
}