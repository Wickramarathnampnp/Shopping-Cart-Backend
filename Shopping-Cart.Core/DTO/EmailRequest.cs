using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.DTO
{
	public  class EmailRequest
	{
		public string? ToEmail { get; set; }
		public string? ToName { get; set; }
		public string? TemplateId { get; set; }
		public string? Subject { get; set; }
		public Object? TemplateData { get; set; }
	}
}
