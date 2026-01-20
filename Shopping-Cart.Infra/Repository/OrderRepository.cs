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
	public  class OrderRepository : IOrderRepository
	{
		public readonly string? _connectionString;

		public OrderRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}
		public async Task<IEnumerable<Order>> GetOrdersAsync(int? OrderId = null, int? CustomerId = null)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			var query = @"SELECT 
                    o.orderid, o.customerid, TO_CHAR(o.orderdate, 'YYYY-MM-DD') as orderdate, 
                    o.totalprice,  
                    oi.orderitemid, oi.orderid AS OrderItem_OrderId, 
                    oi.itemid,i.itemname, oi.unitprice, 
                    oi.quantity, oi.subtotalprice 
                FROM orders o
                LEFT JOIN orderitem oi ON o.orderid = oi.orderid
                LEFT JOIN Item i ON oi.ItemId = i.ItemId
                WHERE o.customerid = @CustomerId AND o.isactive = true
                ORDER BY o.orderdate DESC;";
			var orderDictionary = new Dictionary<int, Order>();
			await connection.QueryAsync<Order, OrderItem, Order>(
			   query,
			   (order, orderItem) =>
			   {
				   if (!orderDictionary.TryGetValue(order.OrderId, out var currentOrder))
				   {
					   currentOrder = order;
					   orderDictionary.Add(order.OrderId, currentOrder);
				   }

				   if (orderItem != null)
				   {
					   currentOrder.Items.Add(orderItem);
				   }

				   return currentOrder;
			   },
			   new { CustomerId = CustomerId },
			   splitOn: "OrderItemId"
		   );
			return orderDictionary.Values.ToList();
		}
		public async Task<Object> AddOrderAsync(AddOrderDto AddOrderDto)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			await connection.OpenAsync();
			using var transaction = await connection.BeginTransactionAsync();
			try
			{
				DateTime date = DateTime.Now;
				var query = "INSERT INTO orders (customerid,orderdate,totalprice) VALUES (@CustomerId,@OrderDate,@TotalPrice) RETURNING orderid;";
				var orderid = await connection.ExecuteScalarAsync(query, new { CustomerId = AddOrderDto.CustomerId, OrderDate = date, TotalPrice = AddOrderDto.TotalPrice },transaction);
				var _query = "INSERT INTO orderitem (orderid,itemid,unitprice,quantity,subtotalprice) VALUES (@OrderId,@ItemId,@UnitPrice,@Quantity,@SubTotalPrice)";
				foreach (var item in AddOrderDto.Items)
				{
					await connection.ExecuteAsync(_query,
						new
						{
							OrderId = orderid,
							ItemId = item.ItemId,
							UnitPrice = item.UnitPrice,
							Quantity = item.Quantity,
							SubTotalPrice = item.SubTotalPrice
						},transaction);
				}
				await transaction.CommitAsync();
				return orderid;
			}
			catch 
			{
				await transaction.RollbackAsync();
				throw ;
			}
		}
	}
}
