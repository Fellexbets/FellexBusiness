using System.Collections.Generic;
using System.Threading.Tasks;
using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class MovementController : BaseController<MovementBusiness, MovementPersistence, Movement>
    {
        public MovementController() : base() => business = new MovementBusiness();


        [HttpGet("{id1}")]
        public async Task<Movement> GetById(int id1) => await business.GetById(id1);

        [HttpDelete("{id1}")]
        public async Task<bool> Delete(int id1) => await business.Delete(id1);


        //[HttpGet("{id}")]
        //public void GettAllMovementsUser(int id)
        //{
            
        //    business.GetAllMovementsUser(id);

        //}

    }
}
