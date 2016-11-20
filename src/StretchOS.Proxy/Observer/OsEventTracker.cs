using StretchOS.Proxy.Events;
using System;
using System.Collections.Generic;

namespace StretchOS.Proxy.Observer
{
	public class OsEventTracker : IObservable<OsEventType>, IEventNotifier
	{
		List<IObserver<OsEventType>> _observers = new List<IObserver<OsEventType>>();

		public void Notify(OsEventType eventType)
		{
			_observers.ForEach(o => o.OnNext(eventType));
		}

		public IDisposable Subscribe(IObserver<OsEventType> observer)
		{
			_observers.Add(observer);

			return null;
		}
	}
}
