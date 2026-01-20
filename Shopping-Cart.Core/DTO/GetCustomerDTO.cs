using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.DTO
{
	public class GetCustomerDto
	{
		public int CustomerId { get; set; }
		public string? Email { get; set; }
	}
}
