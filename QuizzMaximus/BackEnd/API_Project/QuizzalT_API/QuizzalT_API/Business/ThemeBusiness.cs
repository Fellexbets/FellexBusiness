using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class ThemeBusiness : BaseBusiness<ThemePersistence, Theme>
    {
        public ThemeBusiness() => database = new ThemePersistence();

        public async Task<Theme> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);
    }
}
