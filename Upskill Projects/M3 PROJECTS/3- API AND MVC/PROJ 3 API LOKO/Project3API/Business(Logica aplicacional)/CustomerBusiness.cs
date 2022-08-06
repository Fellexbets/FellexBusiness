using Project3API.Models;
using Project3API.Persistence_DB_;


namespace Project3API.Business_Logica_aplicacional_
{
	public class CustomerBusiness : BaseBusiness<CustomerDB, Customer>
	{
		public CustomerBusiness()
		{
			database = new CustomerDB();
		}
	}
}
