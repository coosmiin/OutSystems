using StretchOS.Proxy.Events;

namespace StretchOS.Proxy.Observer
{
	public interface IEventNotifier
	{
		void Notify(OsEventType eventType);
	}
}
