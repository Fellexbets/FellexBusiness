using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project3API.Business_Logica_aplicacional_;
using Project3API.Models;
using Project3API.Persistence_DB_;
using System.Collections.Generic;

namespace Project3API.Controllers
{
	public abstract class BaseController<T, U, V> : ControllerBase where T : BaseBusiness<U, V> where U : BaseDB<V> where V : Entity
	{
		protected T business;
        private ILogger<CustomerController> logger;
        private readonly ILogger<BaseController<T, U, V>> _logger;
		public BaseController(ILogger<BaseController<T, U, V>> logger)
		{
			_logger = logger;
		}

        protected BaseController(ILogger<CustomerController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public List<V> GetAll()
        {
            return business.GetAll();
        }

        [HttpPost]
        public void Create(V entity)
        {
            business.Create(entity);

        }

        [HttpGet("{id}")]

        public V GetById(int id)
        {
            return business.GetById(id);
        }

        [HttpPut]
        public void Update(V entity)
        {
            business.Update(entity);

        }
        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            business.Remove(id);
        }
    }
}
