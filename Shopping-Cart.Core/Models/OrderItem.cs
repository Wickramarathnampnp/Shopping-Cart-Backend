using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Models
{
	public class OrderItem
	{
		public int OrderItemId { get; set; }
		public int OrderId { get; set; }
		public int ItemId { get; set; }
		public string? ItemName { get; set; }
		public float UnitPrice { get; set; }
		public int Quantity { get; set; }
		public float SubTotalPrice { get; set; }
		public bool IsActive { get; set; }

	}
}
