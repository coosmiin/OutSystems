using System.Runtime.InteropServices;
using StretchOS.Proxy.WebProxy;
using StretchOS.Proxy.Parsers;
using StretchOS.Proxy.Sniffers;

namespace StretchOS.Console
{
	using Proxy.Events;
	using Proxy.Observer;
	using System;
	using Console = System.Console;

	class Program
	{
		private static IWebProxy _osWebProxy;

		static void Main(string[] args)
		{
			// on Console exit make sure we also exit the proxy
			SetConsoleCtrlHandler(ConsoleEventCallback, true);

			_osWebProxy = new OsWebProxy();

			var eventTracker = new OsEventTracker();

			eventTracker.Subscribe(new OsBuildObserver());

			_osWebProxy.RegisterSniffer(new OsServiceSniffer(eventTracker, new OsServiceRequestParser()));

			_osWebProxy.StartProxy();

			Console.Read();

			_osWebProxy.StopProxy();

			// TODO: check that it works together with fiddler (ProxyServer.UpStreamHttpProxy)
			// TODO: check what's with the error that happens on Ctrl+C (+ add a big try catch)
		}

		private static bool ConsoleEventCallback(int eventType)
		{
			try
			{
				_osWebProxy.StopProxy();
			}
			catch { /* ignore */ }

			return true;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCtrlHandler(HandlerRoutine callback, bool add);

		internal delegate bool HandlerRoutine(int eventType);
	}
}
