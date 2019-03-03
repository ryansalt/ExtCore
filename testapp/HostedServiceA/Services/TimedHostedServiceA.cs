using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExtCore.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestConsoleApp.Events;

namespace HostedServiceA.Services
{
	public class TimedHostedServiceA : IHostedService, IDisposable
	{
		private readonly ILogger _logger;
		private Timer _timer;
		private readonly IServiceProvider _serviceProvider;

		public TimedHostedServiceA(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
		{
			_logger = loggerFactory.CreateLogger<TimedHostedServiceA>();
			_serviceProvider = serviceProvider;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Time background service 1 is starting");

			_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			Event<ISomeActionEventHandler, string, IServiceProvider>.Broadcast($"{DateTime.Now} SomeActionEvent raised by TimedHostedServiceA", _serviceProvider);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Background TimedHostedServiceA is stopping");
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
