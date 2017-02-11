namespace StretchOS.Proxy.OsEvents
{
	public interface IOsEventHub
	{
		void Publish(IOsEvent osEvent);
		void Start();
		void Stop();
	}
}