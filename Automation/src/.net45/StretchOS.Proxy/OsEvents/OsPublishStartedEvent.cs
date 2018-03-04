namespace StretchOS.Proxy.OsEvents
{
	public class OsPublishStartedEvent : OsEventBase
	{
		public override OsEventType Type { get; } = OsEventType.PublishStarted;
		public int ESpaceId { get; set; }
	}
}
