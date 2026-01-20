using Microsoft.AspNetCore.Mvc;
using Shopping_Cart.Core.Interfaces;

namespace Shopping_Cart.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
	{
		public readonly ICategoryService CategoryService;

		public CategoryController(ICategoryService _CategoryService)
		{
			CategoryService = _CategoryService;
		}

		[HttpGet("GetCategory")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCategory()
		{
			var categories = await CategoryService.CategoryGet();
			return Ok(categories);
		}

	}
}
