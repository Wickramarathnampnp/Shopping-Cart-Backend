using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.DTO
{
	public class AddDeliveryDetailsDto
	{
		public int OrderId { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Line1 { get; set; }
		public string? Line2 { get; set; }
		public string? Line3 { get; set; }
		public string? District { get; set; }
		public int PostalCode { get; set; }
	}
}
