using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Application.Service
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository OrderRepository;
		public OrderService(IOrderRepository _OrderRepository) 
		{
			OrderRepository = _OrderRepository;
		}

		public async Task<IEnumerable<Order>> GetOrder(int? OrderId = null, int? CustomerId = null)
		{
			var orders = await OrderRepository.GetOrdersAsync(OrderId,CustomerId);
			return orders;
		}
		public async Task<Object> AddOrder(AddOrderDto AddOrderDto)
		{
			return await OrderRepository.AddOrderAsync(AddOrderDto);
		}
	}
}
