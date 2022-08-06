using Project3API.Models;

using System;


namespace Project3API.Persistence_DB_
{
	public class CustomerDB : BaseDB<Customer>
	{
		public CustomerDB()
		{
			dbEntity = db.Customer;
		}
	}
}
