using System;
using System.Collections.Concurrent;

namespace StretchOS.Proxy.Events
{
	public class OsEventHub : IOsEventHub
	{
		private BlockingCollection<IOsEvent> _osEvents = new BlockingCollection<IOsEvent>();

		public void Publish(IOsEvent osEvent)
		{
			if (osEvent.Type == OsEventType.BuildStarted)
			{
				_osEvents.Add(osEvent);
			}
		}

		public void Start()
		{
			// this will be an infinite loop as long as the .CompleteAdding() is not called
			foreach (var osEvent in _osEvents.GetConsumingEnumerable())
			{
				if (osEvent.Type == OsEventType.BuildStarted)
				{
					var osBuildStartedEvent = osEvent as OsBuildStartedEvent;

					Console.Write("{0}:", osBuildStartedEvent.ESpaceId);
					string eSpaceName = Console.ReadLine();
					Console.WriteLine(eSpaceName);
				}
			}
		}

		public void Stop()
		{
			_osEvents.CompleteAdding();
		}
	}
}
