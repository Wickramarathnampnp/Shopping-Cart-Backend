using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Models
{
	public class ErrorResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public string? Message { get; set; }

		public ErrorResponse(HttpStatusCode statusCode, string message)
		{
			StatusCode = statusCode;
			Message = message;
		}
	}
}
