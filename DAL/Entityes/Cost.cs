using DAL.Entityes.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entityes
{
	public class Cost : Entity 
	{
		[Column(TypeName = "decimal(18,2)")]
		public decimal? Price { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Max_discount { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Current_discount { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Quantity { get; set; }
	}
}
