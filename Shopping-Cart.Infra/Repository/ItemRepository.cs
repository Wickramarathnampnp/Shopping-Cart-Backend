using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Infra.Repository
{
	public class ItemRepository : IItemRepository 
	{
		private readonly string? _connectionString;

		public ItemRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}

		//Implement filter 
		//

		public async Task<IEnumerable<Item>> GetItemAsync(int? CategoryId = null)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			var query = @"SELECT * from item WHERE 1 = 1 AND isactive = true";
			var parameters = new DynamicParameters();
			if (CategoryId.HasValue)
			{
				query += " AND categoryid = @CategoryId";
				parameters.Add("CategoryId", CategoryId.Value);
			}
			var items = await connection.QueryAsync<Item>(query, parameters);
			return items;
		}
	}
}
