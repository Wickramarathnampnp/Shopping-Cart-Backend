using Moq;
using Shopping_Cart.Application.Service;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using Shopping_Cart.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shopping_Cart.Test
{
	public class GetOrderTest
	{

		private readonly Mock<IOrderRepository> OrderRepositoryMock;
		private readonly IOrderService OrderService;
		public GetOrderTest()
		{
			OrderRepositoryMock = new Mock<IOrderRepository>();
			OrderService = new OrderService(OrderRepositoryMock.Object);
		}
		[Fact]
		public async Task GetOreder_ShouldReturnAll_WhenPrametersNull()
		{
			//Arrange
			int? customerid = null;
			int? orderid = null;

			OrderRepositoryMock
			.Setup(repo => repo.GetOrdersAsync(orderid, customerid))
			.ReturnsAsync(new List<Order>());

			//Act
			var result = await OrderService.GetOrder(orderid,customerid);

			//Assert
			Assert.Empty(result);

		}
	}
}
