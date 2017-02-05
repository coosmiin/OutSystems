using System;
using System.Runtime.InteropServices;
using StretchOS.Proxy.WebProxy;
using StretchOS.Proxy.Parsers;
using StretchOS.Proxy.Sniffers;
using StretchOS.Proxy.Events;
using StretchOS.Proxy.ESpaces;

namespace StretchOS.Console
{
	using Console = System.Console;

	class Program
	{
		private static IWebProxy _osWebProxy;

		private static IOsEventHub _osEventHub = new OsEventHub(new ESpaceCenter());

		static void Main(string[] args)
		{
			// on Console exit make sure we also exit the proxy
			SetConsoleCtrlHandler(ConsoleEventCallback, true);

			_osWebProxy = new OsWebProxy();

			_osWebProxy.RegisterSniffer(
				new OsESpacePublishStartedSniffer(new OsESpacePublishStartedResponseParser(), _osEventHub));
			_osWebProxy.RegisterSniffer(
				new OsESpacePublishStateSniffer(
					new OsCheckESpacePublishStateRequestParser(), new OsCheckESpacePublishStateResponseParser(), _osEventHub));

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
