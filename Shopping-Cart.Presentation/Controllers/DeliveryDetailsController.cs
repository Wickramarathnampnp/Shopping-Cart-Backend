using Microsoft.AspNetCore.Mvc;
using Shopping_Cart.Application.Service;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;

namespace Shopping_Cart.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DeliveryDetailsController : ControllerBase
	{
		private readonly IDeliveryDetailsService DeliveryDetailsService;

		public DeliveryDetailsController(IDeliveryDetailsService _DeliveryDetailsService)
		{
			DeliveryDetailsService = _DeliveryDetailsService;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddDeliveryDetails([FromBody] AddDeliveryDetailsDto AddDeliveryDetailsDto)
		{
			if (AddDeliveryDetailsDto == null)
			{
				return BadRequest();
			}
			await DeliveryDetailsService.AddDeliveryDetails(AddDeliveryDetailsDto);
			return Ok();
		}
	}
}
