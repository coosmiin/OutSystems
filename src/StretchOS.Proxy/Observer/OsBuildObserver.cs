using StretchOS.Proxy.Events;
using System;

namespace StretchOS.Proxy.Observer
{
	public class OsBuildObserver : IObserver<OsEventType>
	{
		public void OnCompleted()
		{
		}

		public void OnError(Exception error)
		{
		}

		public void OnNext(OsEventType eventType)
		{
			if (eventType != OsEventType.BuildStarted)
				return;

			Console.WriteLine("OsBuildObserver: BuildStarted!");
		}
	}
}
