using DAL.Entityes.Base;

namespace DAL.Entityes
{
	public class Manufacturer : NameEntity
	{
		public virtual ICollection<Product> Products { get; set; }
	}
}
