using System.ComponentModel.DataAnnotations;

namespace DAL.Entityes.Base
{
	public abstract class NameEntity : Entity 
	{
		[Required]
		public string? Name { get; set; }
	}

}
