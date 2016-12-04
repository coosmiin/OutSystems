using StretchOS.Proxy.Domain;
using System;
using System.Collections.Generic;

namespace StretchOS.Proxy.Observer
{
	public class OsEventTracker : IObservable<OsRequest>, IObservable<OsResponse>, IEventNotifier
	{
		List<IObserver<OsRequest>> _requestObservers = new List<IObserver<OsRequest>>();
		List<IObserver<OsResponse>> _responseObservers = new List<IObserver<OsResponse>>();

		public void Notify(OsRequest request)
		{
			_requestObservers.ForEach(o => o.OnNext(request));
		}

		public void Notify(OsResponse response)
		{
			_responseObservers.ForEach(o => o.OnNext(response));
		}

		public IDisposable Subscribe(IObserver<OsRequest> observer)
		{
			_requestObservers.Add(observer);

			return null;
		}

		public IDisposable Subscribe(IObserver<OsResponse> observer)
		{
			_responseObservers.Add(observer);

			return null;
		}
	}
}
