using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Models
{
	public class Item
	{
		public int ItemId { get; set; }
		public int CategoryId { get; set; }
		public string? ItemName { get; set; }
		public float UnitPrice { get; set; }
		public bool IsActive { get; set; }

	}
}
