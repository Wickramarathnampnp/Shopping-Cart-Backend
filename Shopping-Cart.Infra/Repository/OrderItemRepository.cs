using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Infra.Repository
{
	public class OrderItemRepository : IOrderItemRepository
	{
		public readonly string? _connectionString;

		public OrderItemRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}
		
		public async Task<int> AddOrderItemAsync(AddOrderItemDto AddOrderItemDto)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			var query = "INSERT INTO orderitem (orderid,itemid,unitprice,quantity,subtotalprice) VALUES (@OrderId,@ItemId,@UnitPrice,@Quantity,@SubTotalPrice)";
			return await connection.ExecuteAsync(query,new {OrderId = AddOrderItemDto.OrderId,ItemId = AddOrderItemDto.ItemId,UnitPrice = AddOrderItemDto.UnitPrice,Quantity = AddOrderItemDto.Quantity,SubTotalPrice = AddOrderItemDto.SubTotalPrice});
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemAsync(int OrderId)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			var query = "SELECT * FROM orderitem WHERE orderid = @OrderId";
			var orderitems = await connection.QueryAsync<OrderItem>(query, new { OrderId = OrderId });
			return orderitems;
		}
	}
}
