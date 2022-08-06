using Project3API.Models;
using Project3API.Persistence_DB_;

namespace Project3API.Business_Logica_aplicacional_
{
	public class ShipperBusiness : BaseBusiness<ShipperDB, Shipper>
	{
		public ShipperBusiness()
		{
			database = new ShipperDB();
		}
	}
}
