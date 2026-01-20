using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Interfaces
{
	public  interface IOrderRepository
	{
		Task<IEnumerable<Order>> GetOrdersAsync(int? OrderId = null, int? CustomerId = null);
		Task<Object> AddOrderAsync(AddOrderDto AddOrderDto);
	}
}
