using StretchOS.Proxy;

namespace StretchOS.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var osProxy = new OsProxy();

			osProxy.StartProxy();

			// TODO: the namespace is the same; can we solve this somehow?
			System.Console.Read();

			// TODO: make sure the proxy settings are removed when the console app is closed
			// TODO: check that it works together with fiddler
		}
	}
}
