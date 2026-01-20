using Shopping_Cart.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Interfaces
{
	public interface IDeliveryDetailsRepository
	{
		Task<int> AddDeliveryDetailsAsync(AddDeliveryDetailsDto AddDeliveryDetailsDto);
	}
}
