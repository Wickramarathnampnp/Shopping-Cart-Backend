using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Application.Service
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository CategoryRepository;
		public CategoryService(ICategoryRepository _CategoryRepository)
		{
			CategoryRepository = _CategoryRepository;
		}

		public async Task<IEnumerable<Category>> CategoryGet()
		{
			var categories = await CategoryRepository.GetAllCategoryAsync();
			return categories;
		}
	}
}
