using Microsoft.Extensions.Hosting;
using Shopping_Cart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Infra.Background_Service
{
	public class EmailBackgroundService  : BackgroundService
	{
		private readonly IEmailQueueService EmailQueueService;
		private readonly IEmailService EmailService;
		public EmailBackgroundService(IEmailQueueService _EmailQueueService, IEmailService _EmailService)
		{
			EmailQueueService = _EmailQueueService;
			EmailService = _EmailService;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					var emailRequest = await EmailQueueService.DequeueEmailAsync(stoppingToken);

					if (emailRequest != null)
					{
						await EmailService.SendEmail(
						   emailRequest   
					   );
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error in EmailBackgroundService: {ex.Message}");
				}
			}
		}
	}
}
