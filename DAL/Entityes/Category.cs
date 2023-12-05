using DAL.Entityes.Base;

namespace DAL.Entityes
{
	public class Category : NameEntity
	{
		public virtual ICollection<Product> Products { get; set; }
	}
}
