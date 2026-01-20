using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.DTO
{
	public class AddOrderDto
	{
		public int CustomerId { get; set; }
		public float TotalPrice { get; set; }

		public List<OrderItem> Items { get; set; } = new List<OrderItem>();
	}
}
