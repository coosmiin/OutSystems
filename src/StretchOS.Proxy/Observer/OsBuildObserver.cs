using StretchOS.Proxy.Domain;
using StretchOS.Proxy.Events;
using System;

namespace StretchOS.Proxy.Observer
{
	public class OsBuildObserver : IObserver<OsRequest>
	{
		public void OnCompleted()
		{
		}

		public void OnError(Exception error)
		{
		}

		public void OnNext(OsRequest request)
		{
			if (request.Type != OsRequestType.Build)
				return;

			Console.WriteLine("OsBuildObserver: BuildStarted!");
		}
	}
}
