using System;
using ExtensionA.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestConsoleApp.Events;

namespace ExtensionA
{
	public class SomeEventHandler : ISomeActionEventHandler
	{
		public int Priority => 1000;

		public void HandleEvent(string argument, IServiceProvider provider)
		{
			var service = provider.GetService<ITestService>();
			service.Print($"{argument} :: reported by {nameof(ExtensionA)}.{nameof(SomeEventHandler)}");
		}
	}
}
