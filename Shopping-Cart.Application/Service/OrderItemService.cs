using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Application.Service
{
	public  class OrderItemService : IOrderItemService
	{
		private readonly IOrderItemRepository OrderItemRepository;

		public OrderItemService(IOrderItemRepository _OrderItemRepository)
		{
			OrderItemRepository = _OrderItemRepository;
		}

		public async Task AddOrderItem(AddOrderItemDto AddOrderItemDto)
		{
			await OrderItemRepository.AddOrderItemAsync(AddOrderItemDto);
		}
		public async Task GetOrder(int OrderId)
		{
			await OrderItemRepository.GetOrderItemAsync(OrderId);
		}
	}
}
