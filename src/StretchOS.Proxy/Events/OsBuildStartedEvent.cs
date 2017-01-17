namespace StretchOS.Proxy.Events
{
	public class OsBuildStartedEvent : IOsEvent
	{
		public OsEventType Type { get; } = OsEventType.BuildStarted;
		public int ESpaceId { get; set; }
	}
}
