using System.Collections.Generic;
using System.Threading.Tasks;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class TransferController : BaseController<TransferBusiness, TransferPersistence, Transfer>
    {
        public TransferController() : base() => business = new TransferBusiness();


        [HttpGet("{id1},{id2},{id3}")]
        public async Task<Transfer> GetById(int id1, int id2, int id3) => await business.GetById(id1, id2, id3);

        [HttpDelete("{id1},{id2},{id3}")]
        public async Task<bool> Delete(int id1, int id2, int id3) => await business.Delete(id1, id2, id3);


        [HttpGet("{id}")]
        public void GettAllTransfersUser(int id) => business.GetAllTransfersUser(id);

    }
}
