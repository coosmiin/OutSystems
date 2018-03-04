using System;
using System.Collections.Concurrent;
using StretchOS.Proxy.Domain;
using StretchOS.Proxy.ESpaces;

namespace StretchOS.Proxy.OsEvents
{
	public class OsEventHub : IOsEventHub
	{
		private IESpaceCenter _eSpaceCenter;

		private BlockingCollection<IOsEvent> _osEvents = new BlockingCollection<IOsEvent>();
		private ConcurrentDictionary<int, OsPublishTransaction> _transactions = new ConcurrentDictionary<int, OsPublishTransaction>();

		public OsEventHub(IESpaceCenter eSpaceCenter)
		{
			_eSpaceCenter = eSpaceCenter;
		}

		public void Publish(IOsEvent osEvent)
		{
			_osEvents.Add(osEvent);
		}

		public void Start()
		{
			// this will be an infinite loop as long as the .CompleteAdding() is not called
			foreach (var osEvent in _osEvents.GetConsumingEnumerable())
			{
				if (osEvent.Type == OsEventType.PublishStarted)
				{
					var osPublishStartedEvent = osEvent as OsPublishStartedEvent;

					_transactions.TryAdd(
						osEvent.VersionId,
						new OsPublishTransaction
						{
							VersionId = osPublishStartedEvent.VersionId,
							ESpaceId = osPublishStartedEvent.ESpaceId
						});

					Console.WriteLine($"Publish started for eSpace with id: {osPublishStartedEvent.ESpaceId}");

					//string eSpaceName = Console.ReadLine();
					//Console.WriteLine(eSpaceName);

					continue;
				}

				if (osEvent.Type == OsEventType.PublishCompleted)
				{
					var osPublishCompletedEvent = osEvent as OsPublishCompletedEvent;

					OsPublishTransaction transaction = _transactions[osEvent.VersionId];

					_eSpaceCenter.AddOrUpdateESpace(
						new ESpace(transaction.ESpaceId, osPublishCompletedEvent.ESpaceName));

					_eSpaceCenter.AddConsumers(transaction.ESpaceId, osPublishCompletedEvent.ConsumerNames);

					_eSpaceCenter.SaveState();

					Console.WriteLine($"Publish completed for: {osPublishCompletedEvent.ESpaceName}, VersionId: {osPublishCompletedEvent.VersionId}");
					Console.WriteLine($"The following eSpaces are outdated: {string.Join(", ", osPublishCompletedEvent.ConsumerNames)}");

					continue;
				}

				throw new Exception($"EventType: {osEvent.Type} cannot be handled");
			}
		}

		public void Stop()
		{
			_osEvents.CompleteAdding();
			_eSpaceCenter.SaveState();
		}
	}
}
