namespace StretchOS.Proxy.Events
{
	public interface IOsEvent
	{
		OsEventType Type { get; }
		int VersionId { get; }
	}
}
