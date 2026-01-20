using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.DTO
{
	public class AddCustomerDto
	{
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Line1 { get; set; }
		public string? Line2 { get; set; }
		public string? Line3 { get; set; }
		public int PostalCode { get; set; }
	}
}
