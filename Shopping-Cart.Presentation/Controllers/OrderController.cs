using Microsoft.AspNetCore.Mvc;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Core.Models;

namespace Shopping_Cart.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService OrderService;
		private readonly IEmailQueueService EmailQueueService;
		private readonly ICustomerService CustomerService;
		public OrderController(IOrderService _OrderService, IEmailQueueService _EmailQueueService, ICustomerService _CustomerService)
		{
			OrderService = _OrderService;
			EmailQueueService = _EmailQueueService;
			CustomerService = _CustomerService;
		}
		[HttpGet("GetOrder")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetOrder([FromQuery] int? OrderId, [FromQuery] int? CustomerId)
		{
			var orders = await OrderService.GetOrder(OrderId,CustomerId);
			return Ok(orders);
		}
		[HttpPost("AddOrder")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> AddOrder(AddOrderDto AddOrderDto)
		{
			EmailRequest emailRequest = new EmailRequest();
			if (AddOrderDto == null)
			{
				return BadRequest();
			}
			var customer = await CustomerService.GetCustomer(AddOrderDto.CustomerId,null);
			if (customer == null)
			{
				BadRequest();
			}
			emailRequest.TemplateId = "d-5b776f6560044afdb53a1a4ac14f3a47";
			emailRequest.Subject = "Order Confirmation - Thank You for Your Purchase!";
			emailRequest.ToEmail = customer.Email;
			emailRequest.ToName = customer.Name;
			emailRequest.TemplateData = AddOrderDto;
			EmailQueueService.EnqueueEmail(emailRequest);
			var orderid = await OrderService.AddOrder(AddOrderDto);
			return Ok(new {message = orderid});
		}
	}
}
