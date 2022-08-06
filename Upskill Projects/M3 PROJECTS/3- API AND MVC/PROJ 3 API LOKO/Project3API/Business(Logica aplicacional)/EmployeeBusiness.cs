using Project3API.Models;
using Project3API.Persistence_DB_;


namespace Project3API.Business_Logica_aplicacional_
{
	public class EmployeeBusiness : BaseBusiness<EmployeeDB, Employee>
	{
		public EmployeeBusiness()
		{
			database = new EmployeeDB();
		}
	}
}
