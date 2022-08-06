using Project3API.Models;

using System;


namespace Project3API.Persistence_DB_
{
	public class ProductDB : BaseDB<Product>
	{
		public ProductDB()
		{
			dbEntity = db.Product;
		}
	}
}
