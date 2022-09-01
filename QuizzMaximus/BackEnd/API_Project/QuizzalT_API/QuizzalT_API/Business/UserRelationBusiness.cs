using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class UserRelationBusiness : BaseBusiness<UserRelationPersistence, UserRelation>
    {
        public UserRelationBusiness() => database = new UserRelationPersistence();

        public async Task<UserRelation> GetById(int id1, int id2) => await database.GetById(id1, id2);
        public async Task<bool> Delete(int id1, int id2) => await database.Delete(id1, id2);

        public async Task<List<UserRelation>> GetAllRelationsUser(int id) => await database.GetAllRelationsUser(id);

    }
}
