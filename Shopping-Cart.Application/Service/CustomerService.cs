using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Application.Service
{
	public  class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository CustomerRepository;
		public CustomerService(ICustomerRepository _CustomerRepository)
		{
			CustomerRepository = _CustomerRepository;
		}
		public async Task AddCustomer(AddCustomerDto AddCustomerDto)
		{
			 await CustomerRepository.AddCustomerAsync( AddCustomerDto);
		}
		public async Task<Customer> GetCustomer(int? CustomerId = null, string? Email = null)
		{
			var customer = await CustomerRepository.GetCustomerAsysnc(CustomerId, Email);
			return customer;
		}
	}
}
