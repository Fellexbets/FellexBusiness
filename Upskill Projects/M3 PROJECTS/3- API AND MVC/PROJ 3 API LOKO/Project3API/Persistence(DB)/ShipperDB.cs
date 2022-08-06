using Project3API.Models;

using System;


namespace Project3API.Persistence_DB_
{
	public class ShipperDB : BaseDB<Shipper>
	{
		public ShipperDB()
		{
			dbEntity = db.Shipper;
		}
	}
}
