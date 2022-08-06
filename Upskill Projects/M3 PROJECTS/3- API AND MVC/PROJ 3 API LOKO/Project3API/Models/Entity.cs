using System.ComponentModel.DataAnnotations;

namespace Project3API.Models
{
	public abstract class Entity
	{
		[Key]
		public int Id { get; set; }
	}
}
