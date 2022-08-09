using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Igor_AIS_Proj.Business
{
    public class TransferBusiness : BaseBusiness<TransferPersistence, Transfer>
    {
        public TransferBusiness() => database = new TransferPersistence();


        public async Task<Transfer> GetById(int id1, int id2, int id3) => await database.GetById(id1, id2, id3);
        public async Task<bool> Delete(int id1, int id2, int id3) => await database.Delete(id1, id2, id3);

        public void GetAllTransfersUser (int id) => database.GetAllTransfersUser(id);

    }
}
