using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using StretchOS.Proxy.WebProxy;
using StretchOS.Proxy.Parsers;
using StretchOS.Proxy.Sniffers;
using StretchOS.Proxy.ServiceProxy;

namespace StretchOS.Console
{
	using Proxy.Events;
	using Console = System.Console;

	class Program
	{
		private static IWebProxy _osWebProxy;

		private static IOsEventHub _osEventHub = new OsEventHub();

		static void Main(string[] args)
		{
			// on Console exit make sure we also exit the proxy
			SetConsoleCtrlHandler(ConsoleEventCallback, true);

			_osWebProxy = new OsWebProxy();

			_osWebProxy.RegisterSniffer(
				new OsESpaceBuildSniffer(new ServiceCenterProxy(), new OsEspaceBuildResponseParser(), _osEventHub));

			_osWebProxy.StartProxy();
			_osEventHub.Start();

			// TODO: check that it works together with fiddler (ProxyServer.UpStreamHttpProxy)			
		}

		private static bool ConsoleEventCallback(int eventType)
		{
			try
			{
				_osWebProxy.StopProxy();
				_osEventHub.Stop();
			}
			catch (Exception ex)
			{
				// TODO: check what's with the error that happens on Ctrl+C
				Console.WriteLine(ex.Message);
			}

			return true;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCtrlHandler(HandlerRoutine callback, bool add);

		internal delegate bool HandlerRoutine(int eventType);
	}
}
