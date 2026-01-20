using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Infra.Service
{
	public class EmailService : IEmailService
	{
		private readonly string? EmailKey;
		public EmailService(IConfiguration configuration)
		{
			EmailKey = configuration["SendGrid:ApiKey"];
		}
		public async Task SendEmail( EmailRequest EmailRequest)
		{
			var Client = new SendGridClient(EmailKey);
			var FromEmail = "pamudu.21@cse.mrt.ac.lk";
			var FromName = "Book.lk";

			var From = new EmailAddress(FromEmail, FromName);
			var To = new EmailAddress(EmailRequest.ToEmail, EmailRequest.ToName);

			var msg = new SendGridMessage()
			{
				From = From,
				Subject = EmailRequest.Subject 
			};
			msg.AddTo(To);
			msg.SetTemplateId(EmailRequest.TemplateId);
			msg.SetTemplateData(EmailRequest.TemplateData);
			await Client.SendEmailAsync(msg);

		}
	}
}
