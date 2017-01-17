namespace StretchOS.Proxy.Events
{
	public interface IOsEventHub
	{
		void Publish(IOsEvent osEvent);
		void Start();
		void Stop();
	}
}