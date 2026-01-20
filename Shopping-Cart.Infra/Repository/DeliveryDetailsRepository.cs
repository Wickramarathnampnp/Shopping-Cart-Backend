using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Infra.Repository
{
	public class DeliveryDetailsRepository : IDeliveryDetailsRepository
	{
		public readonly string? _connectionString;

		public DeliveryDetailsRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}

		public async Task<int> AddDeliveryDetailsAsync(AddDeliveryDetailsDto AddDeliveryDetailsDto)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			var query = "INSERT INTO deliverydetails (orderid,phonenumber,line1,line2,line3,district,postalcode) VALUES (@OrderId,@PhoneNumber,@Line1,@Line2,@Line3,@District,@PostalCode)";
			return await connection.ExecuteAsync(query, new { 
				OrderId = AddDeliveryDetailsDto.OrderId, 
				PhoneNumber = AddDeliveryDetailsDto.PhoneNumber,
				Line1 = AddDeliveryDetailsDto.Line1, 
				Line2 = AddDeliveryDetailsDto.Line2, 
				Line3 = AddDeliveryDetailsDto.Line3, 
				District = AddDeliveryDetailsDto.District,
				PostalCode = AddDeliveryDetailsDto.PostalCode
			});
		}
	}
}
