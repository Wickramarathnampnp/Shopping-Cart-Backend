using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public string? OrderDate { get; set; }
		public float TotalPrice { get; set; }
		public bool IsActive { get; set; }

		public List<OrderItem> Items { get; set; } = new List<OrderItem>();

	}
}
