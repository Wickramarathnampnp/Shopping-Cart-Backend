using Microsoft.AspNetCore.Mvc;
using Shopping_Cart.Application.Service;
using Shopping_Cart.Core.Interfaces;

namespace Shopping_Cart.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ItemController : ControllerBase
	{
		public readonly IItemService ItemService;

		public ItemController(IItemService _ItemService)
		{
			ItemService = _ItemService;
		}
		[HttpGet("GetItems")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetItem([FromQuery] int? CategoryId)
		{
			var categories = await ItemService.GetItems(CategoryId);
			return Ok(categories);
		}

	}
}
