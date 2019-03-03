using ExtensionA.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExtensionA.Services
{
	public class TestService : ITestService
	{
		private readonly ILogger _logger;

		public TestService(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<TestService>();
		}

		public void Print(string message)
		{
			_logger.LogInformation(message);
		}
	}
}
