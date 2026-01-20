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
	public class CustomerRepository : ICustomerRepository 
	{
		public readonly string? _connectionString;

		public CustomerRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}

		public async Task<int> AddCustomerAsync(AddCustomerDto AddCustomerDto)
		{
			using var connection = new NpgsqlConnection( _connectionString );
			var query = "INSERT INTO customer (name,email,line1,line2,line3,postalcode) VALUES (@Name,@Email,@Lane1,@Lane2,@Lane3,@PostalCode)";
			return await connection.ExecuteAsync(query, new { 
				Name = AddCustomerDto.Name,
				Email = AddCustomerDto.Email, 
				Lane1 = AddCustomerDto.Line1, 
				Lane2 = AddCustomerDto.Line2,
				Lane3 = AddCustomerDto.Line3,
				PostalCode = AddCustomerDto.PostalCode 
			});

		}
		public async Task<Customer> GetCustomerAsysnc(int? CustomerId = null, string? Email = null)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			var query = @"SELECT *  FROM customer WHERE 1=1";
			var parameters = new DynamicParameters();

			if (CustomerId.HasValue)
			{
				query += " AND customerid = @CustomerId";
				parameters.Add("CustomerId", CustomerId.Value);
			}
			if (Email != null) 
			{
				query += " AND email = @Email";
				parameters.Add("Email", Email);
			}
			var customer = await connection.QuerySingleAsync<Customer>(query, parameters);

			return customer;
		}
	}
}
