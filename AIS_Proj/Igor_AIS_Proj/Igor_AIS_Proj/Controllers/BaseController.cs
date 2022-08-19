using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Igor_AIS_Proj.Persistence;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Business;

namespace Igor_AIS_Proj.Controllers
{
    public class BaseController<V, U, T> : ControllerBase where V : BaseBusiness<U, T> where U : BasePersistence<T> where T : Entity
    {
        protected V business;

        public BaseController() { }


        [HttpGet]
        public virtual  List<T> GetAll() =>  business.GetAll();


        [HttpPost, AllowAnonymous]
        public virtual async Task<T> Create(T entity) => await business.Create(entity);

        [HttpPut]
        public virtual async Task<bool> Update(T entity) => await business.Update(entity);

        [HttpDelete]
        public virtual async Task<bool> Delete(T entity) => await business.Delete(entity);





    }
}
