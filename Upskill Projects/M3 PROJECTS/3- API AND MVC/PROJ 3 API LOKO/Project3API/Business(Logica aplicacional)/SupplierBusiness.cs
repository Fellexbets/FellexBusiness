using Project3API.Models;
using Project3API.Persistence_DB_;


namespace Project3API.Business_Logica_aplicacional_
{
	public class SupplierBusiness : BaseBusiness<SupplierDB, Supplier>
	{
		public SupplierBusiness()
		{
			database = new SupplierDB();
		}
	}
}
