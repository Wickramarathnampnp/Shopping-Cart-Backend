using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Application.Service
{
	public class ItemService : IItemService
	{
		private readonly IItemRepository ItemRepository;
		public ItemService(IItemRepository _ItemRepository)
		{
			ItemRepository = _ItemRepository;
		}
		public async Task<IEnumerable<Item>> GetItems(int? CategoryId = null)
		{
			var items = await ItemRepository.GetItemAsync(CategoryId);
			return items;
		}
	}
}
