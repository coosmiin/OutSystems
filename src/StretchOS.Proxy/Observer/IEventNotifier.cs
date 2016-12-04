using StretchOS.Proxy.Domain;

namespace StretchOS.Proxy.Observer
{
	public interface IEventNotifier
	{
		void Notify(OsRequest request);
		void Notify(OsResponse response);
	}
}
