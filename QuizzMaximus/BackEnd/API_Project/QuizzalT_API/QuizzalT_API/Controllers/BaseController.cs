using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using QuizzalT_API.Business;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace QuizzalT_API.Controllers
{
    public class BaseController<V, U, T> : ControllerBase where V : BaseBusiness<U, T> where U : BasePersistence<T> where T : Entity
    {
        protected V business;

        public BaseController() { }


        [HttpGet]
        public virtual async Task<List<T>> GetAll() => await business.GetAll();


        [HttpPost, AllowAnonymous]
        public virtual async Task<T> Create(T entity) => await business.Create(entity);

        [HttpPut]
        public virtual async Task<bool> Update(T entity) => await business.Update(entity);

        [HttpDelete]
        public virtual async Task<bool> Delete(T entity) => await business.Delete(entity);





    }
}
