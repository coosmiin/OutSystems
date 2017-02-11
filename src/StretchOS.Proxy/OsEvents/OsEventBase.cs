namespace StretchOS.Proxy.OsEvents
{
	public abstract class OsEventBase : IOsEvent
	{
		public abstract OsEventType Type { get; }
		public int VersionId { get; set; }
	}
}
