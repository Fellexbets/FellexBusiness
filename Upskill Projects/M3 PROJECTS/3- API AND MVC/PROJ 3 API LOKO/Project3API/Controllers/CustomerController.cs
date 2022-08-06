using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project3API.Business_Logica_aplicacional_;
using Project3API.Models;
using Project3API.Persistence_DB_;


namespace Project3API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class CustomerController : BaseController<CustomerBusiness, CustomerDB, Customer>
	{
		public CustomerController(ILogger<CustomerController> logger) : base(logger)
		{
			business = new CustomerBusiness();
		}
	}
}
