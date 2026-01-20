using Moq;
using Shopping_Cart.Application.Service;
using Shopping_Cart.Core.DTO;
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
	public class AddOrderTest
	{
		private readonly Mock<IOrderRepository> OrderRepositoryMock;
		private readonly IOrderService OrderService;

		public AddOrderTest()
		{
			OrderRepositoryMock = new Mock<IOrderRepository>();	
			OrderService =  new OrderService(OrderRepositoryMock.Object);
		}

		[Fact]
		public async Task AddOrder_ShouldInvokeRepository_WhenCallWithValidData()
		{
			//Arrange
			var orderItems = new List<OrderItem>
			{
				new OrderItem
				{
					OrderItemId = 1,
					OrderId = 101,
					ItemId = 1001,
					ItemName = "Laptop",
					UnitPrice = 1500.00f,
					Quantity = 2,
					SubTotalPrice = 3000.00f,
					IsActive = true
				},
				new OrderItem
				{
					OrderItemId = 2,
					OrderId = 101,
					ItemId = 1002,
					ItemName = "Mouse",
					UnitPrice = 20.00f,
					Quantity = 1,
					SubTotalPrice = 20.00f,
					IsActive = true
				},
				new OrderItem
				{
					OrderItemId = 3,
					OrderId = 102,
					ItemId = 1003,
					ItemName = "Keyboard",
					UnitPrice = 50.00f,
					Quantity = 1,
					SubTotalPrice = 50.00f,
					IsActive = false
				},
				new OrderItem
				{
					OrderItemId = 4,
					OrderId = 103,
					ItemId = 1004,
					ItemName = "Monitor",
					UnitPrice = 200.00f,
					Quantity = 3,
					SubTotalPrice = 600.00f,
					IsActive = true
				}
			};

			var AddOrderDto = new AddOrderDto
			{
				CustomerId = 1,
				TotalPrice = 1000.00f,
				Items = orderItems
			};

			OrderRepositoryMock
		   .Setup(repo => repo.AddOrderAsync(It.Is<AddOrderDto>(dto =>
			 dto.CustomerId == AddOrderDto.CustomerId &&
				 dto.TotalPrice == AddOrderDto.TotalPrice &&
				 dto.Items.SequenceEqual(AddOrderDto.Items))));
	       

			//Act
			await OrderService.AddOrder(AddOrderDto);
			//Assert 
			OrderRepositoryMock.Verify(repo => repo.AddOrderAsync(It.IsAny<AddOrderDto>()), Times.Once);
		}
		[Fact]
		public async Task AddOrder_ShouldThrowException_WhenCallWithoutValidData()
		{
			//Arrange
			var orderItems = new List<OrderItem>
			{
				new OrderItem
				{
					OrderItemId = 1,
					OrderId = 101,
					ItemId = 1001,
					ItemName = "Laptop",
					UnitPrice = 1500.00f,
					Quantity = 2,
					SubTotalPrice = 3000.00f,
					IsActive = true
				},
				new OrderItem
				{
					OrderItemId = 2,
					OrderId = 101,
					ItemId = 1002,
					ItemName = "Mouse",
					UnitPrice = 20.00f,
					Quantity = 1,
					SubTotalPrice = 20.00f,
					IsActive = true
				},
				new OrderItem
				{
					OrderItemId = 3,
					OrderId = 102,
					ItemId = 1003,
					ItemName = "Keyboard",
					UnitPrice = 50.00f,
					Quantity = 1,
					SubTotalPrice = 50.00f,
					IsActive = false
				},
				new OrderItem
				{
					OrderItemId = 4,
					OrderId = 103,
					ItemId = 1004,
					ItemName = "Monitor",
					UnitPrice = 200.00f,
					Quantity = 3,
					SubTotalPrice = 600.00f,
					IsActive = true
				}
			};

			var AddOrderDto = new AddOrderDto
			{
				CustomerId = 1,
				TotalPrice = 1000.00f,
			};

			//Act and Assert
			await Assert.ThrowsAsync<System.Exception>(() => OrderService.AddOrder(AddOrderDto));
			OrderRepositoryMock.Verify(repo => repo.AddOrderAsync(AddOrderDto), Times.Once);
		}
	}
}
