namespace StretchOS.Proxy.OsEvents
{
	public interface IOsEvent
	{
		OsEventType Type { get; }
		int VersionId { get; }
	}
}
