using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Igor_AIS_Proj.Business
{
    public class UserBusiness : BaseBusiness<UserPersistence, User>
    {
        public UserBusiness() => database = new UserPersistence();
        public async Task<User> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);




    }
}
