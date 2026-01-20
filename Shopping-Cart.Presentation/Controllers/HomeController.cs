using Microsoft.AspNetCore.Mvc;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using Shopping_Cart.Infra.Service;

namespace Shopping_Cart.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly IEmailQueueService EmailQueueService;
		public HomeController(IEmailQueueService _EmailQueueService)
		{
			EmailQueueService = _EmailQueueService;
		}
		[HttpPost("EnqueueEmail")]
		[ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult EnqueueEmail([FromBody] EmailRequest emailRequest)
		{
		    EmailQueueService.EnqueueEmail(emailRequest);
			return Ok(new { message = "Email has been added to the queue" });
		}
	}
}
