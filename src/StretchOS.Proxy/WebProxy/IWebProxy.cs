using StretchOS.Proxy.Sniffers;

namespace StretchOS.Proxy.WebProxy
{
	public interface IWebProxy
	{
		void StartProxy();
		void StopProxy();
		void RegisterSniffer(IWebSniffer sniffer);
	}
}