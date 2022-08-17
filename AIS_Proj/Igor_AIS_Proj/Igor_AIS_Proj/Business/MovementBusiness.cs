using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Igor_AIS_Proj.Business
{
    public class MovementBusiness : BaseBusiness<MovementPersistence,  Movement>
    {
        public MovementBusiness() => database = new MovementPersistence();


        public async Task<Movement> GetById(int id1) => await database.GetById(id1);
        public async Task<bool> Delete(int id1) => await database.Delete(id1);

        //public void GetAllMovementsUser(int id) => database.GetAllMovementsUser(id);

    }
}
