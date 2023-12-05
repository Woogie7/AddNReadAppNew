using DAL.Entityes.Base;

namespace DAL.Entityes
{
	public class Provider : NameEntity
	{
		public virtual ICollection<Product> Products { get; set; }
	}
}
