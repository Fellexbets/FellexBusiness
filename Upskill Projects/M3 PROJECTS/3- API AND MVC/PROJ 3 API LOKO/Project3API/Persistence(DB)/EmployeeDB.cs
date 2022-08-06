using Project3API.Models;
using System;


namespace Project3API.Persistence_DB_
{
	public class EmployeeDB : BaseDB<Employee>
	{
		public EmployeeDB()
		{
			dbEntity = db.Employee;
		}
	}
}
