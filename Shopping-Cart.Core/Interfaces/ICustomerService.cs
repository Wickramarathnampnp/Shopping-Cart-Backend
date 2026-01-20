using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Interfaces
{
	public  interface ICustomerService
	{
		Task AddCustomer(AddCustomerDto AddCustomerDto);
		Task<Customer> GetCustomer(int? CustomerId = null, string? Email = null);
	}
}
