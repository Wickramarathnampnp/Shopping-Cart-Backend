using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Interfaces
{
	public interface ICustomerRepository
	{
		Task<int> AddCustomerAsync(AddCustomerDto AddCustomerDto);
		Task<Customer> GetCustomerAsysnc(int? CustomerId = null, string? Email = null);
	}
}
