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
	public class CategoryRepository : ICategoryRepository
	{
		private readonly string? _connectionString;

		public CategoryRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}

		public async Task<IEnumerable<Category>> GetAllCategoryAsync()
		{
			using var connecction = new NpgsqlConnection(_connectionString);
			var query = "SELECT categoryid,categoryname FROM category WHERE isactive = true";
			var categories = await connecction.QueryAsync<Category>(query);
			return categories;
		}
	}
}
