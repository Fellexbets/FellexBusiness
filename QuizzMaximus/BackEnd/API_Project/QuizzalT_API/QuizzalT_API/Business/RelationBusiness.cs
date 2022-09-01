using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class RelationBusiness : BaseBusiness<RelationPersistence, Relation>
    {
        public RelationBusiness() => database = new RelationPersistence();

        public async Task<Relation> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);
    }
}
