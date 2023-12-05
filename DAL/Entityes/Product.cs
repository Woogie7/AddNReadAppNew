using DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entityes
{
	public class Product : NameEntity
	{
		public virtual Category Category { get; set; }
		public virtual Provider Provider { get; set; }
		public virtual Manufacturer Manufacturer { get; set; }
		public string? Description { get; set; }
		public string? Articul { get; set; }

	}
}
