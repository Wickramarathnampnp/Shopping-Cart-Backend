using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Application.Service
{
	public class DeliveryDetailsService: IDeliveryDetailsService
	{
		private readonly IDeliveryDetailsRepository DeliveryDetailsRepository;

		public DeliveryDetailsService(IDeliveryDetailsRepository _DeliveryDetailsRepository)
		{
			DeliveryDetailsRepository = _DeliveryDetailsRepository;
		}

		public async Task AddDeliveryDetails(AddDeliveryDetailsDto AddDeliveryDetailsDto)
		{
			await DeliveryDetailsRepository.AddDeliveryDetailsAsync(AddDeliveryDetailsDto);
		}
	}
}
