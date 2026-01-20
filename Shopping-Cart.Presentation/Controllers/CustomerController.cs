using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;

namespace Shopping_Cart.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService CustomerService;
		public CustomerController(ICustomerService _CustomerService)
		{

			CustomerService = _CustomerService;
		}

		[HttpPost("AddCustomer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddCustomer([FromBody] AddCustomerDto AddCustomerDto)
		{
			if (AddCustomerDto == null) 
			{ 
				return BadRequest(); 
			}
			await CustomerService.AddCustomer(AddCustomerDto);
			return Ok();
		}
		[HttpGet("GetCustomer")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCustomer()
		{
			if (User.Identity?.IsAuthenticated != true)
			{
				return Unauthorized();
			}
			var Email = User.Claims.FirstOrDefault(c => c.Type == "https://www.novestra2.us/email")?.Value;
			if (string.IsNullOrEmpty(Email))
			{
				return NotFound("Email claim not found in the access token.");
			}
			var customer = await CustomerService.GetCustomer( null ,Email);
			if (customer == null) 
			{ 
				return BadRequest();
			}
			return Ok(customer);
		}
	}
}
