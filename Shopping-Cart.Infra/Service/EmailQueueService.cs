using Shopping_Cart.Core.DTO;
using Shopping_Cart.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart.Infra.Service
{
	//Make this generic
	public class EmailQueueService : IEmailQueueService
	{
		private readonly ConcurrentQueue<EmailRequest> _emailQueue = new();
		private readonly SemaphoreSlim _signal = new(0);



		public void EnqueueEmail(EmailRequest emailRequest)
		{
			_emailQueue.Enqueue(emailRequest);
			_signal.Release();
		}
		public async Task<EmailRequest?> DequeueEmailAsync(CancellationToken cancellationToken)
		{
			await _signal.WaitAsync(cancellationToken);

			if (_emailQueue.TryDequeue(out var emailRequest))
			{
				return emailRequest;
			}

			return null;
		}

	}
}
