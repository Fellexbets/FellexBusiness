using Project3API.Models;

using System;


namespace Project3API.Persistence_DB_
{
	public class SupplierDB : BaseDB<Supplier>
	{
		public SupplierDB()
		{
			dbEntity = db.Supplier;
		}
	}
}
