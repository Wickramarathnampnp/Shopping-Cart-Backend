using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Core.Interfaces
{
	public interface IItemRepository
	{
		Task<IEnumerable<Item>> GetItemAsync(int? CategoryId = null);
	}
}
